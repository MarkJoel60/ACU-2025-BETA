// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APAdjust
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP.Standalone;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.MigrationMode;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.TX;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// Records the fact of application of one Accounts Payable document to another,
/// which results in an adjustment of the balances of both documents. It can be either
/// an application of a <see cref="T:PX.Objects.AP.APPayment">payment document</see> to an
/// <see cref="T:PX.Objects.AP.APAdjust.APInvoice">invoice document</see> (for example, when a check
/// closes a bill), or an application of one <see cref="T:PX.Objects.AP.APPayment">payment</see> document
/// to another, such as an application of a vendor refund to a prepayment. The entities
/// of this type are mainly edited on the Checks and Payments (AP302000) form,
/// which corresponds to the <see cref="T:PX.Objects.AP.APPaymentEntry" /> graph. They can also be edited
/// on the Applications tab of the Bills and Adjustments (AP301000) form, which corresponds
/// to the <see cref="T:PX.Objects.AP.APInvoiceEntry" /> graph.
/// </summary>
[PXPrimaryGraph(new System.Type[] {typeof (APQuickCheckEntry), typeof (APPaymentEntry)}, new System.Type[] {typeof (Select<APQuickCheck, Where<APQuickCheck.docType, Equal<Current<APAdjust.adjgDocType>>, And<APQuickCheck.refNbr, Equal<Current<APAdjust.adjgRefNbr>>>>>), typeof (Select<APPayment, Where<APPayment.docType, Equal<Current<APAdjust.adjgDocType>>, And<APPayment.refNbr, Equal<Current<APAdjust.adjgRefNbr>>>>>)})]
[PXCacheName("Adjust")]
[DebuggerDisplay("{AdjdDocType}:{AdjdRefNbr}:{AdjdLineNbr} - {AdjgDocType}:{AdjgRefNbr}:{AdjNbr}")]
[Serializable]
public class APAdjust : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAdjustment,
  IDocumentAdjustment,
  IAdjustmentAmount,
  IAdjustmentStub,
  IFinAdjust
{
  protected bool? _Selected = new bool?(false);
  protected bool? _SeparateCheck;
  protected int? _VendorID;
  protected 
  #nullable disable
  string _AdjgDocType;
  protected string _AdjgRefNbr;
  protected int? _AdjgBranchID;
  protected long? _AdjdCuryInfoID;
  public const string EmptyApDocType = "---";
  protected string _AdjdDocType;
  protected int? _AdjdBranchID;
  protected int? _AdjNbr;
  protected string _StubNbr;
  protected string _AdjBatchNbr;
  protected int? _VoidAdjNbr;
  protected long? _AdjdOrigCuryInfoID;
  protected long? _AdjgCuryInfoID;
  protected System.DateTime? _AdjgDocDate;
  protected string _AdjgFinPeriodID;
  protected string _AdjgTranPeriodID;
  protected System.DateTime? _AdjdDocDate;
  protected string _AdjdFinPeriodID;
  protected string _AdjdTranPeriodID;
  protected Decimal? _CuryAdjgDiscAmt;
  protected Decimal? _CuryAdjgWhTaxAmt;
  protected Decimal? _CuryAdjgAmt;
  protected Decimal? _AdjDiscAmt;
  protected Decimal? _CuryAdjdDiscAmt;
  protected Decimal? _AdjWhTaxAmt;
  protected Decimal? _CuryAdjdWhTaxAmt;
  protected Decimal? _AdjAmt;
  protected Decimal? _RGOLAmt;
  protected bool? _Released;
  protected bool? _Hold;
  protected bool? _Voided;
  protected int? _AdjdAPAcct;
  protected int? _AdjdAPSub;
  protected int? _AdjdWhTaxAcctID;
  protected int? _AdjdWhTaxSubID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected Guid? _NoteID;
  protected Decimal? _AdjdCuryRate;
  protected Decimal? _CuryDocBal;
  protected Decimal? _DocBal;
  protected Decimal? _CuryDiscBal;
  protected Decimal? _DiscBal;
  protected Decimal? _CuryWhTaxBal;
  protected Decimal? _WhTaxBal;
  protected Decimal? _CuryAdjdPPDAmt;

  /// <summary>
  /// Indicates whether the record is selected for mass processing or not.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the adjusted document should be paid for by a separate check.
  /// (See <see cref="!:APInvoice.SeparateCheck" />)
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Pay Separately", Visibility = PXUIVisibility.Visible)]
  public virtual bool? SeparateCheck
  {
    get => this._SeparateCheck;
    set => this._SeparateCheck = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.AP.Vendor" />, whom the related documents belongs.
  /// </summary>
  [Vendor(Visibility = PXUIVisibility.Visible, Visible = false)]
  [PXDBDefault(typeof (APPayment.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  /// <summary>The type of the adjusting document.</summary>
  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [PXDBDefault(typeof (APPayment.docType))]
  [PXUIField(DisplayName = "AdjgDocType", Visibility = PXUIVisibility.Visible, Visible = false)]
  public virtual string AdjgDocType
  {
    get => this._AdjgDocType;
    set => this._AdjgDocType = value;
  }

  /// <summary>
  /// The type of the adjusting document for printing. Internal representation is the same as for <see cref="P:PX.Objects.AP.APAdjust.AdjgDocType" />,
  /// but the user-friendly values of these two fields differ.
  /// </summary>
  [PXString(3, IsFixed = true)]
  [APDocType.PrintList]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.Visible, Enabled = true)]
  public virtual string PrintAdjgDocType
  {
    get => this._AdjgDocType;
    set
    {
    }
  }

  /// <summary>Reference number of the adjusting document.</summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (APPayment.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
  [PXParent(typeof (Select<APRegister, Where<APRegister.docType, Equal<Current<APAdjust.adjgDocType>>, And<APRegister.refNbr, Equal<Current<APAdjust.adjgRefNbr>>, And<Current<APAdjust.released>, NotEqual<True>>>>>))]
  public virtual string AdjgRefNbr
  {
    get => this._AdjgRefNbr;
    set => this._AdjgRefNbr = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch" />, to which the adjusting document belongs.
  /// </summary>
  [Branch(typeof (APPayment.branchID), null, true, true, true)]
  public virtual int? AdjgBranchID
  {
    get => this._AdjgBranchID;
    set => this._AdjgBranchID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">Currency Info</see> record associated with the adjusted document.
  /// </summary>
  [PXDBLong]
  [PX.Objects.CM.Extensions.CurrencyInfo(typeof (APRegister.curyInfoID))]
  public virtual long? AdjdCuryInfoID
  {
    get => this._AdjdCuryInfoID;
    set => this._AdjdCuryInfoID = value;
  }

  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string AdjdCuryID { get; set; }

  /// <summary>The type of the adjusted document.</summary>
  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [PXDefault("INV")]
  [PXUIField(DisplayName = "Document Type", Visibility = PXUIVisibility.Visible)]
  [APInvoiceType.TaxInvoiceList]
  public virtual string AdjdDocType
  {
    get => this._AdjdDocType;
    set => this._AdjdDocType = value;
  }

  /// <summary>
  /// The type of the adjusted document for printing. Internal representation is the same as for <see cref="P:PX.Objects.AP.APAdjust.AdjgDocType" />,
  /// but the user-friendly values of these two fields differ.
  /// </summary>
  [PXString(3, IsFixed = true)]
  [APDocType.PrintList]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.Visible, Enabled = true)]
  public virtual string PrintAdjdDocType
  {
    get => this._AdjdDocType;
    set
    {
    }
  }

  /// <summary>Reference number of the adjusted document.</summary>
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.Visible)]
  [APInvoiceType.AdjdRefNbr(typeof (Search2<APAdjust.APInvoice.refNbr, LeftJoin<APAdjust, On<APAdjust.adjdDocType, Equal<APAdjust.APInvoice.docType>, And<APAdjust.adjdRefNbr, Equal<APAdjust.APInvoice.refNbr>, And<APAdjust.released, Equal<False>, And<APAdjust.voided, Equal<False>, PX.Data.And<Where<APAdjust.adjgDocType, NotEqual<Current<APPayment.docType>>, Or<APAdjust.adjgRefNbr, NotEqual<Current<APPayment.refNbr>>>>>>>>>, LeftJoin<APAdjust2, On<APAdjust2.adjgDocType, Equal<APAdjust.APInvoice.docType>, And<APAdjust2.adjgRefNbr, Equal<APAdjust.APInvoice.refNbr>, And<APAdjust2.released, Equal<False>, And<APAdjust2.voided, Equal<False>>>>>, LeftJoin<APPayment, On<APPayment.docType, Equal<APAdjust.APInvoice.docType>, And<APPayment.refNbr, Equal<APAdjust.APInvoice.refNbr>, PX.Data.And<Where<APPayment.docType, Equal<APDocType.prepayment>, Or<APPayment.docType, Equal<APDocType.debitAdj>>>>>>>>>, Where<APAdjust.APInvoice.vendorID, Equal<Optional<APPayment.vendorID>>, And<APAdjust.APInvoice.docType, Equal<Optional<APAdjust.adjdDocType>>, And2<Where<APAdjust.APInvoice.released, Equal<True>, Or<APRegister.prebooked, Equal<True>>>, And<APAdjust.APInvoice.openDoc, Equal<True>, And<APRegister.hold, Equal<False>, And<APAdjust.adjgRefNbr, PX.Data.IsNull, And2<Where<APAdjust2.adjdRefNbr, PX.Data.IsNull, Or<APAdjust.APInvoice.docType, Equal<APDocType.prepaymentInvoice>>>, And2<Where<APAdjust.APInvoice.docType, NotEqual<APDocType.prepaymentInvoice>, Or<APRegister.pendingPayment, Equal<True>>>, And2<Where<APPayment.refNbr, PX.Data.IsNull, And<Current<APPayment.docType>, NotEqual<APDocType.refund>, Or<APPayment.refNbr, PX.Data.IsNotNull, And<Current<APPayment.docType>, Equal<APDocType.refund>, Or<APPayment.docType, Equal<APDocType.debitAdj>, And<Current<APPayment.docType>, Equal<APDocType.check>, Or<APPayment.docType, Equal<APDocType.debitAdj>, And<Current<APPayment.docType>, Equal<APDocType.voidCheck>, Or<APPayment.refNbr, PX.Data.IsNotNull, And<Current<APPayment.docType>, Equal<APDocType.debitAdj>>>>>>>>>>>, And2<Where<APAdjust.APInvoice.docDate, LessEqual<Current<APPayment.adjDate>>, And<APAdjust.APInvoice.tranPeriodID, LessEqual<Current<APPayment.adjTranPeriodID>>, Or<Current<APPayment.adjTranPeriodID>, PX.Data.IsNull, Or<Current<APPayment.docType>, Equal<APDocType.check>, And<Current<APSetup.earlyChecks>, Equal<True>, Or<Current<APPayment.docType>, Equal<APDocType.voidCheck>, And<Current<APSetup.earlyChecks>, Equal<True>, Or<Current<APPayment.docType>, Equal<APDocType.prepayment>, And<Current<APSetup.earlyChecks>, Equal<True>>>>>>>>>>, And2<Where<Current<APSetup.migrationMode>, NotEqual<True>, Or<APAdjust.APInvoice.isMigratedRecord, Equal<Current<APRegister.isMigratedRecord>>>>, PX.Data.And<Where<APAdjust.APInvoice.pendingPPD, NotEqual<True>, Or<Current<APRegister.pendingPPD>, Equal<True>>>>>>>>>>>>>>>>), Filterable = true)]
  public virtual string AdjdRefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.", Visibility = PXUIVisibility.Visible, FieldClass = "PaymentsByLines")]
  [PXDefault(typeof (Switch<Case<Where<Selector<APAdjust.adjdRefNbr, APAdjust.APInvoice.paymentsByLinesAllowed>, NotEqual<True>>, PX.Objects.CS.int0>, Null>))]
  [APInvoiceType.AdjdLineNbr]
  public virtual int? AdjdLineNbr { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch" />, to which the adjusted document belongs.
  /// </summary>
  [Branch(null, null, true, true, false, Enabled = false)]
  public virtual int? AdjdBranchID
  {
    get => this._AdjdBranchID;
    set => this._AdjdBranchID = value;
  }

  /// <summary>The number of the adjustment.</summary>
  /// <value>
  /// Defaults to the current <see cref="!:APPayment.AdjCntr">number of lines</see> in the related payment document.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Adjustment Nbr.", Visibility = PXUIVisibility.Visible, Visible = false, Enabled = false)]
  [PXDefault(typeof (APPayment.adjCntr))]
  public virtual int? AdjNbr
  {
    get => this._AdjNbr;
    set => this._AdjNbr = value;
  }

  [PXDBInt]
  public virtual int? CashAccountID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string PaymentMethodID { get; set; }

  /// <summary>The number of the payment stub.</summary>
  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Check Number")]
  public virtual string StubNbr
  {
    get => this._StubNbr;
    set => this._StubNbr = value;
  }

  /// <summary>
  /// The number of the <see cref="T:PX.Objects.GL.Batch" /> generated from the adjustment.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Number", Visibility = PXUIVisibility.Visible, Visible = true, Enabled = false)]
  public virtual string AdjBatchNbr
  {
    get => this._AdjBatchNbr;
    set => this._AdjBatchNbr = value;
  }

  /// <summary>The reference number of the voiding adjustment.</summary>
  [PXDBInt]
  public virtual int? VoidAdjNbr
  {
    get => this._VoidAdjNbr;
    set => this._VoidAdjNbr = value;
  }

  /// <summary>
  /// Identifier of the original <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">Currency Info</see> record associated with the adjusted document.
  /// </summary>
  [PXDBLong]
  [PX.Objects.CM.Extensions.CurrencyInfo(typeof (APPayment.curyInfoID))]
  public virtual long? AdjdOrigCuryInfoID
  {
    get => this._AdjdOrigCuryInfoID;
    set => this._AdjdOrigCuryInfoID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">Currency Info</see> record associated with the adjusting document.
  /// </summary>
  [PXDBLong]
  [PX.Objects.CM.Extensions.CurrencyInfo(typeof (APPayment.curyInfoID))]
  public virtual long? AdjgCuryInfoID
  {
    get => this._AdjgCuryInfoID;
    set => this._AdjgCuryInfoID = value;
  }

  /// <summary>The date when the payment is applied.</summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.AP.APPayment.AdjDate">application date specified on the adjusting document</see>.
  /// </value>
  [PXDBDate]
  [PXDBDefault(typeof (APPayment.adjDate))]
  [PXUIField(DisplayName = "Transaction Date")]
  public virtual System.DateTime? AdjgDocDate
  {
    get => this._AdjgDocDate;
    set => this._AdjgDocDate = value;
  }

  /// <summary>Financial period of payment application.</summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.AP.APPayment.AdjFinPeriodID">application period specified on the adjusting document</see>.
  /// </value>
  [FinPeriodID(null, typeof (APAdjust.adjgBranchID), null, null, null, null, true, false, null, typeof (APAdjust.adjgTranPeriodID), typeof (APPayment.adjTranPeriodID), true, true)]
  [PXUIField(DisplayName = "Application Period", Enabled = false)]
  public virtual string AdjgFinPeriodID
  {
    get => this._AdjgFinPeriodID;
    set => this._AdjgFinPeriodID = value;
  }

  /// <summary>
  /// Financial period of the adjusting document.
  /// This field corresponds to the <see cref="P:PX.Objects.AP.APPayment.AdjTranPeriodID" /> (not user-overridable) field of the adjusting document.
  /// </summary>
  [PeriodID(null, null, null, true)]
  [PXUIField(DisplayName = "Post Period")]
  public virtual string AdjgTranPeriodID
  {
    get => this._AdjgTranPeriodID;
    set => this._AdjgTranPeriodID = value;
  }

  /// <summary>
  /// Either the date when the adjusted document was created or the date of the original vendor’s document.
  /// </summary>
  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual System.DateTime? AdjdDocDate
  {
    get => this._AdjdDocDate;
    set => this._AdjdDocDate = value;
  }

  /// <summary>
  /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial period</see> of the adjusted document. Corresponds to the <see cref="P:PX.Objects.AP.APRegister.FinPeriodID" /> field.
  /// </summary>
  /// <value>
  /// The value of this field is determined from the <see cref="P:PX.Objects.AP.APAdjust.AdjdDocDate" /> field.
  /// </value>
  [FinPeriodID(null, typeof (APAdjust.adjdBranchID), null, null, null, null, true, false, null, typeof (APAdjust.adjdTranPeriodID), null, true, true)]
  [PXUIField(DisplayName = "Post Period", Enabled = false)]
  public virtual string AdjdFinPeriodID
  {
    get => this._AdjdFinPeriodID;
    set => this._AdjdFinPeriodID = value;
  }

  /// <summary>
  /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial period</see> of the adjusted document. Corresponds to the <see cref="P:PX.Objects.AP.APRegister.TranPeriodID" /> field.
  /// </summary>
  [PeriodID(null, null, null, true)]
  public virtual string AdjdTranPeriodID
  {
    get => this._AdjdTranPeriodID;
    set => this._AdjdTranPeriodID = value;
  }

  /// <summary>
  /// The amount of the cash discount taken for the adjusting document.
  /// Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />.
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APAdjust.adjgCuryInfoID), typeof (APAdjust.adjDiscAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cash Discount Taken", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? CuryAdjgDiscAmt
  {
    get => this._CuryAdjgDiscAmt;
    set => this._CuryAdjgDiscAmt = value;
  }

  /// <summary>
  /// The amount of withholding tax calculated for the adjusting document, if applicable.
  /// Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />.
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APAdjust.adjgCuryInfoID), typeof (APAdjust.adjWhTaxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "With. Tax", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? CuryAdjgWhTaxAmt
  {
    get => this._CuryAdjgWhTaxAmt;
    set => this._CuryAdjgWhTaxAmt = value;
  }

  /// <summary>
  /// The actual amount paid on the document.
  /// Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />.
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APAdjust.adjgCuryInfoID), typeof (APAdjust.adjAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount Paid", Visibility = PXUIVisibility.Visible)]
  [PXUnboundFormula(typeof (Mult<APAdjust.adjgBalSign, APAdjust.curyAdjgAmt>), typeof (SumCalc<APPayment.curyApplAmt>))]
  public virtual Decimal? CuryAdjgAmt
  {
    get => this._CuryAdjgAmt;
    set => this._CuryAdjgAmt = value;
  }

  /// <summary>
  /// The amount of the cash discount for the adjusted document.
  /// Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cash Discount Amount")]
  public virtual Decimal? AdjDiscAmt
  {
    get => this._AdjDiscAmt;
    set => this._AdjDiscAmt = value;
  }

  /// <summary>
  /// The amount of the cash discount for the adjusted document.
  /// Presented in the currency of the document, see <see cref="!:CuryID" />.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjdDiscAmt
  {
    get => this._CuryAdjdDiscAmt;
    set => this._CuryAdjdDiscAmt = value;
  }

  /// <summary>
  /// The amount of tax withheld from the payments to the adjusted document.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Withholding Tax Amount")]
  public virtual Decimal? AdjWhTaxAmt
  {
    get => this._AdjWhTaxAmt;
    set => this._AdjWhTaxAmt = value;
  }

  /// <summary>
  /// The amount of tax withheld from the payments to the adjusted document.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjdWhTaxAmt
  {
    get => this._CuryAdjdWhTaxAmt;
    set => this._CuryAdjdWhTaxAmt = value;
  }

  /// <summary>
  /// The amount to be paid for the adjusted document. (See <see cref="P:PX.Objects.AP.APRegister.OrigDocAmt" />)
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? AdjAmt
  {
    get => this._AdjAmt;
    set => this._AdjAmt = value;
  }

  /// <summary>
  /// The amount to be paid for the adjusted document. (See <see cref="P:PX.Objects.AP.APRegister.CuryOrigDocAmt" />)
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjdAmt { get; set; }

  /// <summary>
  /// Realized Gain and Loss amount associated with the adjustment.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Realized Gain/Loss Amount")]
  public virtual Decimal? RGOLAmt
  {
    get => this._RGOLAmt;
    set => this._RGOLAmt = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the adjustment was released.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released")]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the adjustment is on hold.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the adjustment was voided.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  /// <summary>
  /// Identifier of the AP account, to which the adjusted document belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account(SuppressCurrencyValidation = true)]
  [PXDefault]
  public virtual int? AdjdAPAcct
  {
    get => this._AdjdAPAcct;
    set => this._AdjdAPAcct = value;
  }

  /// <summary>
  /// Identifier of the AP subaccount, to which the adjusted document belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount]
  [PXDefault]
  public virtual int? AdjdAPSub
  {
    get => this._AdjdAPSub;
    set => this._AdjdAPSub = value;
  }

  /// <summary>
  /// Identifier of the account associated with withholding tax for the adjusted document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [Account]
  [PXDefault(typeof (Search2<APTaxTran.accountID, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.tranType, Equal<Current<APAdjust.adjdDocType>>, And<APTaxTran.refNbr, Equal<Current<APAdjust.adjdRefNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>, OrderBy<Asc<APTaxTran.taxID>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual int? AdjdWhTaxAcctID
  {
    get => this._AdjdWhTaxAcctID;
    set => this._AdjdWhTaxAcctID = value;
  }

  /// <summary>
  /// Identifier of the subaccount associated with withholding tax for the adjusted document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [SubAccount]
  [PXDefault(typeof (Search2<APTaxTran.subID, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.tranType, Equal<Current<APAdjust.adjdDocType>>, And<APTaxTran.refNbr, Equal<Current<APAdjust.adjdRefNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.withholding>>>>, OrderBy<Asc<APTaxTran.taxID>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual int? AdjdWhTaxSubID
  {
    get => this._AdjdWhTaxSubID;
    set => this._AdjdWhTaxSubID = value;
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

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime
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
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the adjustment.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>Identifier of the Invoice.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AP.APRegister.NoteID">APRegister.NoteID</see> field.
  /// </value>
  [PXDBGuid(false)]
  public virtual Guid? InvoiceID { get; set; }

  /// <summary>Identifier of the Payment.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AP.APRegister.NoteID">APRegister.NoteID</see> field.
  /// </value>
  [PXDBGuid(false)]
  public virtual Guid? PaymentID { get; set; }

  /// <summary>Identifier of the Debit Adjustment.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AP.APRegister.NoteID">APRegister.NoteID</see> field.
  /// </value>
  [PXDBGuid(false)]
  public virtual Guid? MemoID { get; set; }

  /// <summary>
  /// An optional cross rate that can be specified between the currency of the payment and currency of the original document.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXDecimal(8)]
  [PXUIField(DisplayName = "Cross Rate", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? AdjdCuryRate
  {
    get => this._AdjdCuryRate;
    set => this._AdjdCuryRate = value;
  }

  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APAdjust.adjgCuryInfoID), typeof (APAdjust.origDocAmt), BaseCalc = false)]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  [PXDecimal(4)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigDocAmt { get; set; }

  /// <summary>
  /// The amount of the adjustment before the discount is taken.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APAdjust.adjgCuryInfoID), typeof (APAdjust.docBal), BaseCalc = false)]
  [PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual Decimal? CuryDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  /// <summary>
  /// The amount of the adjustment before the discount is taken.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDecimal(4)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  /// <summary>
  /// The difference between the cash discount that was available and the actual amount of cash discount taken.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APAdjust.adjgCuryInfoID), typeof (APAdjust.discBal), BaseCalc = false)]
  [PXUIField(DisplayName = "Cash Discount Balance", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual Decimal? CuryDiscBal
  {
    get => this._CuryDiscBal;
    set => this._CuryDiscBal = value;
  }

  /// <summary>
  /// The difference between the cash discount that was available and the actual amount of cash discount taken.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? DiscBal
  {
    get => this._DiscBal;
    set => this._DiscBal = value;
  }

  /// <summary>
  /// The difference between the amount of the tax to be withheld and the actual withheld amount (if any withholding taxes are applicable).
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APAdjust.adjgCuryInfoID), typeof (APAdjust.whTaxBal), BaseCalc = false)]
  [PXUnboundDefault]
  [PXUIField(DisplayName = "With. Tax Balance", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual Decimal? CuryWhTaxBal
  {
    get => this._CuryWhTaxBal;
    set => this._CuryWhTaxBal = value;
  }

  /// <summary>
  /// The difference between the amount of the tax to be withheld and the actual withheld amount (if any withholding taxes are applicable).
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDecimal(4)]
  [PXUnboundDefault]
  public virtual Decimal? WhTaxBal
  {
    get => this._WhTaxBal;
    set => this._WhTaxBal = value;
  }

  /// <summary>
  /// When equal to <c>true</c>, indicates that the payment was voided or cancelled.
  /// </summary>
  /// <value>
  /// Setting this field to <c>true</c> will change the <see cref="P:PX.Objects.AP.APAdjust.AdjgDocType">type of the adjusting document</see> to Voided Payment (<c>"VCK"</c>)
  /// and set <see cref="P:PX.Objects.AP.APAdjust.Voided" /> to <c>true</c>.
  /// </value>
  [PXBool]
  [PXUIField(DisplayName = "Void Application", Visibility = PXUIVisibility.Visible)]
  [PXDefault(false)]
  public virtual bool? VoidAppl
  {
    [PXDependsOnFields(new System.Type[] {typeof (APAdjust.adjgDocType)})] get
    {
      return new bool?(APPaymentType.VoidAppl(this._AdjgDocType));
    }
    set
    {
      if (!value.Value || APPaymentType.VoidAppl(this.AdjgDocType))
        return;
      this._AdjgDocType = APPaymentType.GetVoidingAPDocType(this.AdjgDocType);
      this.Voided = new bool?(true);
    }
  }

  /// <summary>
  /// A read-only field, which when equal to <c>true</c>, indicates that the sign of the <see cref="P:PX.Objects.AP.APAdjust.RGOLAmt">gain and loss</see> is reversed for the adjustment.
  /// </summary>
  [PXDependsOnFields(new System.Type[] {typeof (APAdjust.adjgDocType), typeof (APAdjust.adjdDocType)})]
  public virtual bool? ReverseGainLoss
  {
    get
    {
      Decimal? adjgTbSign = this.AdjgTBSign;
      Decimal num = -1M;
      return new bool?(adjgTbSign.GetValueOrDefault() == num & adjgTbSign.HasValue);
    }
    set
    {
    }
  }

  /// <summary>
  /// A read-only field showing the sign of the impact of the adjusting document on the balance.
  /// Depends only on the values of the <see cref="P:PX.Objects.AP.APAdjust.AdjgDocType" /> and <see cref="P:PX.Objects.AP.APAdjust.AdjdDocType" /> fields.
  /// </summary>
  [PXDependsOnFields(new System.Type[] {typeof (APAdjust.adjgDocType), typeof (APAdjust.adjdDocType)})]
  public virtual Decimal? AdjgBalSign
  {
    get
    {
      return new Decimal?(this.AdjgDocType == "CHK" && this.AdjdDocType == "ADR" || this.AdjgDocType == "VCK" && this.AdjdDocType == "ADR" ? -1M : 1M);
    }
    set
    {
    }
  }

  /// <summary>
  /// !REV! A read-only field showing the sign of impact of the adjusting document on the General Ledger.
  /// Depends only on the values of the <see cref="P:PX.Objects.AP.APAdjust.AdjgDocType" /> and <see cref="P:PX.Objects.AP.APAdjust.AdjdDocType" /> fields.
  /// </summary>
  [PXDependsOnFields(new System.Type[] {typeof (APAdjust.adjgDocType), typeof (APAdjust.adjgRefNbr), typeof (APAdjust.adjdDocType), typeof (APAdjust.adjdRefNbr)})]
  public virtual Decimal? AdjgGLSign
  {
    get
    {
      int num = this.AdjdDocType == "PPM" ? 1 : 0;
      bool flag1 = this.AdjgDocType == "CHK" || this.AdjgDocType == "VCK";
      bool flag2 = this.AdjgDocType == "PPM" && this.AdjgRefNbr != this.AdjdRefNbr;
      return num == 0 || !(flag1 | flag2) ? APDocType.SignAmount(this.AdjdDocType) : new Decimal?(1M);
    }
    set
    {
    }
  }

  /// <summary>
  /// A read-only field showing the sign of impact of the adjusting document on the trial balance.
  /// Depends only on the values of the <see cref="P:PX.Objects.AP.APAdjust.AdjgDocType" /> and <see cref="P:PX.Objects.AP.APAdjust.AdjdDocType" /> fields.
  /// </summary>
  [PXDependsOnFields(new System.Type[] {typeof (APAdjust.adjgDocType), typeof (APAdjust.adjdDocType)})]
  public virtual Decimal? AdjgTBSign
  {
    get
    {
      if (!this.IsSelfAdjustment())
        return !(this.AdjdDocType == "PPM") || !(this.AdjgDocType == "CHK") && !(this.AdjgDocType == "VCK") && !(this.AdjgDocType == "PPM") ? APDocType.SignBalance(this.AdjdDocType) : new Decimal?(1M);
      Decimal num = -1M;
      Decimal? nullable = APDocType.SignBalance(this.AdjgDocType);
      return !nullable.HasValue ? new Decimal?() : new Decimal?(num * nullable.GetValueOrDefault());
    }
    set
    {
    }
  }

  /// <summary>
  /// A read-only field showing the sign of impact of the adjusted document on the trial balance.
  /// Depends only on the values of the <see cref="P:PX.Objects.AP.APAdjust.AdjgDocType" /> and <see cref="P:PX.Objects.AP.APAdjust.AdjdDocType" /> fields.
  /// </summary>
  [PXDependsOnFields(new System.Type[] {typeof (APAdjust.adjgDocType), typeof (APAdjust.adjdDocType)})]
  public virtual Decimal? AdjdTBSign
  {
    get
    {
      if (this.IsSelfAdjustment())
        return new Decimal?(0M);
      return (!(this.AdjgDocType == "CHK") || !(this.AdjdDocType == "ADR")) && (!(this.AdjgDocType == "VCK") || !(this.AdjdDocType == "ADR")) ? APDocType.SignBalance(this.AdjgDocType) : new Decimal?(1M);
    }
    set
    {
    }
  }

  /// <summary>
  /// A read-only field showing the Realized Gain and Loss amount associated with the adjustment with proper sign.
  /// Depends on the <see cref="P:PX.Objects.AP.APAdjust.RGOLAmt" /> and <see cref="P:PX.Objects.AP.APAdjust.ReverseGainLoss" /> fields.
  /// </summary>
  public virtual Decimal? SignedRGOLAmt
  {
    [PXDependsOnFields(new System.Type[] {typeof (APAdjust.reverseGainLoss), typeof (APAdjust.rGOLAmt)})] get
    {
      if (!this.ReverseGainLoss.Value)
        return this._RGOLAmt;
      Decimal? rgolAmt = this._RGOLAmt;
      return !rgolAmt.HasValue ? new Decimal?() : new Decimal?(-rgolAmt.GetValueOrDefault());
    }
  }

  /// <summary>
  /// A read-only field showing the sign of the impact of the adjusted document on the balance.
  /// Depends only on the values of the <see cref="P:PX.Objects.AP.APAdjust.AdjgDocType" /> and <see cref="P:PX.Objects.AP.APAdjust.AdjdDocType" /> fields.
  /// </summary>
  [PXDependsOnFields(new System.Type[] {typeof (APAdjust.adjgDocType), typeof (APAdjust.adjdDocType)})]
  public virtual Decimal? AdjdBalSign
  {
    get
    {
      return new Decimal?(this.AdjgDocType == "CHK" && this.AdjdDocType == "ADR" || this.AdjgDocType == "VCK" && this.AdjdDocType == "ADR" ? -1M : 1M);
    }
    set
    {
    }
  }

  /// <summary>
  /// The "Tax Doc. Nbr" of the tax transaction, generated on the "Recognize Output/Input VAT" (TX503000/TX503500) form.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxTran.TaxInvoiceNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Doc. Nbr", Enabled = false, Visible = false)]
  public virtual string TaxInvoiceNbr { get; set; }

  [PXString(1, IsFixed = true)]
  [ARAdjust.adjType.List]
  public virtual string AdjType { get; set; }

  [PXDBInt]
  public int? JointPayeeID { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// in migration mode without affecting GL module.
  /// </summary>
  [MigratedRecord(typeof (APSetup.migrationMode))]
  public virtual bool? IsMigratedRecord { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that this is the initial application
  /// created for a migrated document to affect all needed balances.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsInitialApplication { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Subject to Tax Adjustment", Enabled = false, Visible = false)]
  public virtual bool? PendingPPD { get; set; }

  /// <summary>
  /// The reference number of the adjustment, which is generated on the Generate AP Tax Adjustments (AP504500) form.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:APInvoice.RefNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<APAdjust.APInvoice.refNbr, Where<APAdjust.APInvoice.pendingPPD, Equal<True>, PX.Data.And<Where<APAdjust.APInvoice.docType, Equal<APDocType.debitAdj>, Or<APAdjust.APInvoice.docType, Equal<APDocType.creditAdj>>>>>>))]
  public virtual string PPDVATAdjRefNbr { get; set; }

  /// <summary>
  /// The doc type of the adjustment, which is generated on the Generate AP Tax Adjustments (AP504500) form.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="T:PX.Objects.AP.APAdjust.APInvoice.docType" /> field.
  /// </value>
  [PXDBString(3, IsFixed = true, InputMask = "")]
  [APDocType.List]
  public virtual string PPDVATAdjDocType { get; set; }

  /// <summary>
  /// The description of the adjustment, which is generated on the Generate AP Tax Adjustments (AP504500) form.
  /// This field is needed for showing on the Checks and Payments (AP302000) screen like a hyperlink. For example - "Debit Adj., 000084".
  /// </summary>
  /// <value>
  /// Value is <see cref="P:PX.Objects.AP.APAdjust.PPDVATAdjDocType" /> + <see cref="P:PX.Objects.AP.APAdjust.PPDVATAdjRefNbr" /> field.
  /// </value>
  [PXString(40, InputMask = "")]
  [PXUIField(DisplayName = "Tax Adjustment", Enabled = false, Visible = false)]
  public virtual string PPDVATAdjDescription { get; set; }

  [PXDBBool]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<APAdjust.adjdDocType, PX.Data.IsNotNull, And<APAdjust.adjdRefNbr, PX.Data.IsNotNull>>, IsNull<Selector<APAdjust.adjdRefNbr, APRegister.hasPPDTaxes>, False>>, False>))]
  public virtual bool? AdjdHasPPDTaxes { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjPPDAmt { get; set; }

  /// <summary>
  /// The cash discount amount displayed for the document.
  /// Given in the <see cref="!:CuryID"> currency of the adjusted document</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cash Discount Taken", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? CuryAdjdPPDAmt
  {
    get => this._CuryAdjdPPDAmt;
    set => this._CuryAdjdPPDAmt = value;
  }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APAdjust.adjgCuryInfoID), typeof (APAdjust.adjPPDAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Cash Discount Taken", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? CuryAdjgPPDAmt { get; set; }

  [PXString(3, IsFixed = true, InputMask = "")]
  [PXUIField(DisplayName = "Doc. Type")]
  [APDocType.List]
  public virtual string DisplayDocType { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Reference Nbr.")]
  [PXVirtualSelector(typeof (Search<PX.Objects.AP.Standalone.APRegister.refNbr, Where<PX.Objects.AP.Standalone.APRegister.docType, Equal<Current<APAdjust.displayDocType>>>>))]
  public virtual string DisplayRefNbr { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Date", Enabled = false)]
  public virtual System.DateTime? DisplayDocDate { get; set; }

  [PXString(150, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string DisplayDocDesc { get; set; }

  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string DisplayCuryID { get; set; }

  [PXLong]
  public virtual long? DisplayCuryInfoID { get; set; }

  [PXString(6, IsFixed = true)]
  [FinPeriodIDFormatting]
  [PXSelector(typeof (Search<MasterFinPeriod.finPeriodID>))]
  [PXUIField(DisplayName = "Post Period", Enabled = false)]
  public virtual string DisplayFinPeriodID { get; set; }

  [PXString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [APDocStatus.List]
  public virtual string DisplayStatus { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APAdjust.displayCuryInfoID), typeof (APAdjust.adjAmt), BaseCalc = false)]
  [PXUIField(DisplayName = "Amount Paid", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? DisplayCuryAmt { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APAdjust.displayCuryInfoID), typeof (APAdjust.adjDiscAmt), BaseCalc = false)]
  [PXUIField(DisplayName = "Cash Discount Taken", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? DisplayCuryDiscAmt { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APAdjust.displayCuryInfoID), typeof (APAdjust.adjPPDAmt), BaseCalc = false)]
  [PXUIField(DisplayName = "Cash Discount Taken")]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? DisplayCuryPPDAmt { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APAdjust.displayCuryInfoID), typeof (APAdjust.adjWhTaxAmt), BaseCalc = false)]
  [PXUIField(DisplayName = "With. Tax", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? DisplayCuryWhTaxAmt { get; set; }

  string IDocumentAdjustment.Module => "AP";

  Decimal? IAdjustmentAmount.AdjThirdAmount
  {
    get => this.AdjWhTaxAmt;
    set => this.AdjWhTaxAmt = value;
  }

  Decimal? IAdjustmentAmount.CuryAdjgThirdAmount
  {
    get => this.CuryAdjgWhTaxAmt;
    set => this.CuryAdjgWhTaxAmt = value;
  }

  Decimal? IAdjustmentAmount.CuryAdjdThirdAmount
  {
    get => this.CuryAdjdWhTaxAmt;
    set => this.CuryAdjdWhTaxAmt = value;
  }

  public bool Persistent => true;

  public Decimal? CuryOutstandingBalance => new Decimal?();

  public System.DateTime? OutstandingBalanceDate => new System.DateTime?();

  public bool? IsRequest => new bool?();

  public class PK : 
    PrimaryKeyOf<APAdjust>.By<APAdjust.adjgDocType, APAdjust.adjgRefNbr, APAdjust.adjNbr, APAdjust.adjdDocType, APAdjust.adjdRefNbr, APAdjust.adjdLineNbr>
  {
    public static APAdjust Find(
      PXGraph graph,
      string adjgDocType,
      string adjgRefNbr,
      int? adjNbr,
      string adjdDocType,
      string adjdRefNbr,
      int? adjdLineNb,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APAdjust>.By<APAdjust.adjgDocType, APAdjust.adjgRefNbr, APAdjust.adjNbr, APAdjust.adjdDocType, APAdjust.adjdRefNbr, APAdjust.adjdLineNbr>.FindBy(graph, (object) adjgDocType, (object) adjgRefNbr, (object) adjNbr, (object) adjdDocType, (object) adjdRefNbr, (object) adjdLineNb, options);
    }
  }

  public static class FK
  {
    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APAdjust>.By<APAdjust.vendorID>
    {
    }

    public class Payment : 
      PrimaryKeyOf<APRegister>.By<APRegister.docType, APRegister.refNbr>.ForeignKeyOf<APAdjust>.By<APAdjust.adjgDocType, APAdjust.adjgRefNbr>
    {
    }

    public class PaymentCurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APAdjust>.By<APAdjust.adjgCuryInfoID>
    {
    }

    public class PaymentBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<APAdjust>.By<APAdjust.adjgBranchID>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<APRegister>.By<APRegister.docType, APRegister.refNbr>.ForeignKeyOf<APAdjust>.By<APAdjust.adjdDocType, APAdjust.adjdRefNbr>
    {
    }

    public class InvoiceCurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APAdjust>.By<APAdjust.adjdCuryInfoID>
    {
    }

    public class InvoiceBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<APAdjust>.By<APAdjust.adjdBranchID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<PX.Objects.CA.CashAccount>.By<PX.Objects.CA.CashAccount.cashAccountID>.ForeignKeyOf<APAdjust>.By<APAdjust.cashAccountID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<APAdjust>.By<APAdjust.paymentMethodID>
    {
    }

    public class InvoiceWithholdingTaxAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APAdjust>.By<APAdjust.adjdWhTaxAcctID>
    {
    }

    public class InvoiceWithholdingTaxSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<APAdjust>.By<APAdjust.adjdWhTaxSubID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjust.selected>
  {
  }

  public abstract class separateCheck : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjust.separateCheck>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.vendorID>
  {
  }

  public abstract class adjgDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjgDocType>
  {
  }

  public abstract class printAdjgDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjust.printAdjgDocType>
  {
  }

  public abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjgRefNbr>
  {
  }

  public abstract class adjgBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.adjgBranchID>
  {
  }

  public abstract class adjdCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APAdjust.adjdCuryInfoID>
  {
  }

  public abstract class adjdCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjdCuryID>
  {
  }

  public abstract class adjdDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjdDocType>
  {
  }

  public abstract class printAdjdDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjust.printAdjdDocType>
  {
  }

  [PXHidden]
  [PXProjection(typeof (Select2<PX.Objects.AP.Standalone.APRegister, LeftJoin<PX.Objects.AP.Standalone.APInvoice, On<PX.Objects.AP.Standalone.APInvoice.docType, Equal<PX.Objects.AP.Standalone.APRegister.docType>, And<PX.Objects.AP.Standalone.APInvoice.refNbr, Equal<PX.Objects.AP.Standalone.APRegister.refNbr>>>>>))]
  [Serializable]
  public class APInvoice : PX.Objects.AP.Standalone.APRegister
  {
    [PXDBDate(BqlField = typeof (PX.Objects.AP.Standalone.APInvoice.dueDate))]
    [PXUIField(DisplayName = "Due Date")]
    public virtual System.DateTime? DueDate { get; set; }

    [PXDBString(40, IsUnicode = true, BqlField = typeof (PX.Objects.AP.Standalone.APInvoice.invoiceNbr))]
    [PXUIField(DisplayName = "Vendor Ref.")]
    public virtual string InvoiceNbr { get; set; }

    [PXDBBool(BqlField = typeof (PX.Objects.AP.Standalone.APInvoice.isJointPayees))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Joint Payees", FieldClass = "Construction")]
    public bool? IsJointPayees { get; set; }

    public new abstract class docType : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjust.APInvoice.docType>
    {
    }

    public new abstract class refNbr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjust.APInvoice.refNbr>
    {
    }

    public new abstract class vendorID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APAdjust.APInvoice.vendorID>
    {
    }

    public new abstract class released : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APAdjust.APInvoice.released>
    {
    }

    public new abstract class openDoc : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APAdjust.APInvoice.openDoc>
    {
    }

    public new abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APAdjust.APInvoice.docDate>
    {
    }

    public new abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APAdjust.APInvoice.finPeriodID>
    {
    }

    public new abstract class tranPeriodID : IBqlField, IBqlOperand
    {
    }

    public abstract class dueDate : BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APAdjust.APInvoice.dueDate>
    {
    }

    public abstract class invoiceNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APAdjust.APInvoice.invoiceNbr>
    {
    }

    public new abstract class isMigratedRecord : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APAdjust.APInvoice.isMigratedRecord>
    {
    }

    public new abstract class pendingPPD : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APAdjust.APInvoice.pendingPPD>
    {
    }

    public new abstract class paymentsByLinesAllowed : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APAdjust.APInvoice.paymentsByLinesAllowed>
    {
    }

    public abstract class isJointPayees : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APAdjust.APInvoice.isJointPayees>
    {
    }
  }

  public abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjdRefNbr>
  {
  }

  public abstract class adjdLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.adjdLineNbr>
  {
  }

  public abstract class adjdBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.adjdBranchID>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.adjNbr>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.cashAccountID>
  {
  }

  public abstract class paymentMethodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.paymentMethodID>
  {
  }

  public abstract class stubNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.stubNbr>
  {
  }

  public abstract class adjBatchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjBatchNbr>
  {
  }

  public abstract class voidAdjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.voidAdjNbr>
  {
  }

  public abstract class adjdOrigCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    APAdjust.adjdOrigCuryInfoID>
  {
  }

  public abstract class adjgCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APAdjust.adjgCuryInfoID>
  {
  }

  public abstract class adjgDocDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APAdjust.adjgDocDate>
  {
  }

  public abstract class adjgFinPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjgFinPeriodID>
  {
  }

  public abstract class adjgTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjust.adjgTranPeriodID>
  {
  }

  public abstract class adjdDocDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APAdjust.adjdDocDate>
  {
  }

  public abstract class adjdFinPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjdFinPeriodID>
  {
  }

  public abstract class adjdTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjust.adjdTranPeriodID>
  {
  }

  public abstract class curyAdjgDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APAdjust.curyAdjgDiscAmt>
  {
  }

  public abstract class curyAdjgWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APAdjust.curyAdjgWhTaxAmt>
  {
  }

  public abstract class curyAdjgAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.curyAdjgAmt>
  {
  }

  public abstract class adjDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.adjDiscAmt>
  {
  }

  public abstract class curyAdjdDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APAdjust.curyAdjdDiscAmt>
  {
  }

  public abstract class adjWhTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.adjWhTaxAmt>
  {
  }

  public abstract class curyAdjdWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APAdjust.curyAdjdWhTaxAmt>
  {
  }

  public abstract class adjAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.adjAmt>
  {
  }

  public abstract class curyAdjdAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.curyAdjdAmt>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.rGOLAmt>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjust.released>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjust.hold>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjust.voided>
  {
  }

  public abstract class adjdAPAcct : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.adjdAPAcct>
  {
  }

  public abstract class adjdAPSub : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.adjdAPSub>
  {
  }

  public abstract class adjdWhTaxAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.adjdWhTaxAcctID>
  {
  }

  public abstract class adjdWhTaxSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.adjdWhTaxSubID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APAdjust.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APAdjust.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjust.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APAdjust.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APAdjust.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjust.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APAdjust.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APAdjust.noteID>
  {
  }

  public abstract class invoiceID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APAdjust.invoiceID>
  {
  }

  public abstract class paymentID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APAdjust.paymentID>
  {
  }

  public abstract class memoID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APAdjust.memoID>
  {
  }

  public abstract class adjdCuryRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.adjdCuryRate>
  {
  }

  public abstract class curyOrigDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.curyOrigDocAmt>
  {
  }

  public abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.origDocAmt>
  {
  }

  public abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.curyDocBal>
  {
  }

  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.docBal>
  {
  }

  public abstract class curyDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.curyDiscBal>
  {
  }

  public abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.discBal>
  {
  }

  public abstract class curyWhTaxBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.curyWhTaxBal>
  {
  }

  public abstract class whTaxBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.whTaxBal>
  {
  }

  public abstract class voidAppl : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjust.voidAppl>
  {
  }

  public abstract class reverseGainLoss : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjust.reverseGainLoss>
  {
  }

  public abstract class adjgBalSign : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.adjgBalSign>
  {
  }

  public abstract class adjgGLSign : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.adjgGLSign>
  {
  }

  public abstract class adjgTBSign : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.adjgTBSign>
  {
  }

  public abstract class adjdTBSign : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.adjdTBSign>
  {
  }

  public abstract class adjdBalSign : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.adjdBalSign>
  {
  }

  public abstract class taxInvoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.taxInvoiceNbr>
  {
  }

  public abstract class adjType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.adjType>
  {
  }

  public abstract class jointPayeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APAdjust.jointPayeeID>
  {
  }

  public abstract class isMigratedRecord : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjust.isMigratedRecord>
  {
  }

  public abstract class isInitialApplication : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APAdjust.isInitialApplication>
  {
  }

  public abstract class pendingPPD : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjust.pendingPPD>
  {
  }

  public abstract class pPDVATAdjRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.pPDVATAdjRefNbr>
  {
  }

  public abstract class pPDVATAdjDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjust.pPDVATAdjDocType>
  {
  }

  public abstract class pPDVATAdjDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjust.pPDVATAdjDescription>
  {
  }

  public abstract class adjdHasPPDTaxes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjust.adjdHasPPDTaxes>
  {
  }

  public abstract class adjPPDAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.adjPPDAmt>
  {
  }

  public abstract class curyAdjdPPDAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.curyAdjdPPDAmt>
  {
  }

  public abstract class curyAdjgPPDAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.curyAdjgPPDAmt>
  {
  }

  /// <summary>
  /// These fields are required to display two-way (incoming and outgoing)
  /// applications in a same grid: e.g. displaying the adjusting document
  /// number in case of an incoming application, and adjusted document number
  /// in case of an outgoing application. The fields are controlled by specific
  /// BLCs, e.g. filled out in delegates or by formula.
  /// </summary>
  public abstract class displayDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.displayDocType>
  {
  }

  public abstract class displayRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.displayRefNbr>
  {
  }

  public abstract class displayDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APAdjust.displayDocDate>
  {
  }

  public abstract class displayDocDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.displayDocDesc>
  {
  }

  public abstract class displayCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.displayCuryID>
  {
  }

  public abstract class displayCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APAdjust.displayCuryInfoID>
  {
  }

  public abstract class displayFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjust.displayFinPeriodID>
  {
  }

  public abstract class displayStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjust.displayStatus>
  {
  }

  public abstract class displayCuryAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APAdjust.displayCuryAmt>
  {
  }

  public abstract class displayCuryDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APAdjust.displayCuryDiscAmt>
  {
  }

  public abstract class displayCuryPPDAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APAdjust.displayCuryPPDAmt>
  {
  }

  public abstract class displayCuryWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APAdjust.displayCuryWhTaxAmt>
  {
  }
}
