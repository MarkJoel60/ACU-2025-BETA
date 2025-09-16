// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRegister
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
using PX.Objects.AR.BQL;
using PX.Objects.AR.Standalone;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.MigrationMode;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.TM;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// The base class for all accounts receivable documents, which
/// provides the fields common to documents of <see cref="T:PX.Objects.AR.ARInvoice" />,
/// <see cref="T:PX.Objects.AR.ARPayment" />, and <see cref="T:PX.Objects.AR.ARCashSaleEntry" /> types.
/// </summary>
[PXPrimaryGraph(new System.Type[] {typeof (SOInvoiceEntry), typeof (ARCashSaleEntry), typeof (ARInvoiceEntry), typeof (ARPaymentEntry), typeof (ARInvoiceEntry)}, new System.Type[] {typeof (Select<ARInvoice, Where<ARInvoice.docType, Equal<Current<ARRegister.docType>>, And<ARInvoice.refNbr, Equal<Current<ARRegister.refNbr>>, And<ARInvoice.origModule, Equal<BatchModule.moduleSO>, And<ARInvoice.released, Equal<False>>>>>>), typeof (Select<ARCashSale, Where<ARCashSale.docType, Equal<Current<ARRegister.docType>>, And<ARCashSale.refNbr, Equal<Current<ARRegister.refNbr>>>>>), typeof (Select<ARInvoice, Where<ARInvoice.docType, Equal<Current<ARRegister.docType>>, And<ARInvoice.refNbr, Equal<Current<ARRegister.refNbr>>>>>), typeof (Select<ARPayment, Where<ARPayment.docType, Equal<Current<ARRegister.docType>>, And<ARPayment.refNbr, Equal<Current<ARRegister.refNbr>>>>>), typeof (Select<ARInvoice, Where<True, Equal<False>>>)})]
[ARRegisterCacheName("AR Document")]
[DebuggerDisplay("DocType = {DocType}, RefNbr = {RefNbr}")]
[PXGroupMask(typeof (InnerJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARRegister.customerID>, And<Match<Customer, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class ARRegister : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  PX.Objects.CM.IRegister,
  IDocumentKey,
  IAssign,
  IBalance,
  INotable
{
  protected bool? _Selected = new bool?(false);
  protected int? _BranchID;
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected string _OrigModule;
  protected DateTime? _DocDate;
  protected DateTime? _OrigDocDate;
  protected DateTime? _DueDate;
  protected string _TranPeriodID;
  protected string _FinPeriodID;
  protected int? _CustomerID;
  protected int? _CustomerLocationID;
  protected string _CuryID;
  protected int? _ARAccountID;
  protected int? _ARSubID;
  protected int? _LineCntr;
  protected int? _AdjCntr;
  protected long? _CuryInfoID;
  protected Decimal? _CuryOrigDocAmt;
  protected Decimal? _OrigDocAmt;
  protected Decimal? _CuryDocBal;
  protected Decimal? _DocBal;
  protected Decimal? _CuryOrigDiscAmt;
  protected Decimal? _OrigDiscAmt;
  protected Decimal? _CuryDiscTaken;
  protected Decimal? _DiscTaken;
  protected Decimal? _CuryDiscBal;
  protected Decimal? _DiscBal;
  protected Decimal? _DocDisc;
  protected Decimal? _CuryDocDisc;
  protected Decimal? _CuryChargeAmt;
  protected Decimal? _ChargeAmt;
  protected string _DocDesc;
  protected string _TaxCalcMode;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;
  protected short? _BatchSeq;
  protected bool? _Released;
  protected bool? _OpenDoc;
  protected bool? _Hold;
  protected bool? _Scheduled;
  protected bool? _Voided;
  protected Guid? _NoteID;
  protected Guid? _RefNoteID;
  protected string _ClosedFinPeriodID;
  protected string _ClosedTranPeriodID;
  protected Decimal? _RGOLAmt;
  protected string _ScheduleID;
  protected string _ImpRefNbr;
  protected DateTime? _StatementDate;
  protected int? _SalesPersonID;
  protected string _OrigDocType;
  protected string _Status;
  protected Decimal? _CuryDiscountedDocTotal;
  protected Decimal? _DiscountedDocTotal;
  protected Decimal? _CuryDiscountedTaxableTotal;
  protected Decimal? _DiscountedTaxableTotal;
  protected Decimal? _CuryDiscountedPrice;
  protected Decimal? _DiscountedPrice;
  protected bool? _HasPPDTaxes;
  protected bool? _PendingPPD;

  /// <summary>
  /// Indicates whether the record is selected for processing.
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
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> to which the document belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(null, null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>
  /// The type of the document.
  /// This field is a part of the compound key of the document.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocType.ListAttribute" />.
  /// </value>
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  [ARDocType.List]
  [PXUIField]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Document Type (Internal)")]
  public string InternalDocType
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARRegister.docType)})] get => this.DocType;
  }

  /// <summary>
  /// The type of the document for printing, which is used in reports.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocType.PrintListAttribute" />.
  /// </value>
  [PXString(3, IsFixed = true)]
  [ARDocType.PrintList]
  [PXUIField]
  public virtual string PrintDocType
  {
    get => this._DocType;
    set
    {
    }
  }

  /// <summary>
  /// The reference number of the document.
  /// This field is a part of the compound key of the document.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<ARRegister.refNbr, Where<ARRegister.docType, Equal<Optional<ARRegister.docType>>>>), Filterable = true)]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXString(50, IsUnicode = true)]
  [PXUIField]
  [PXDBCalced(typeof (BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<ARRegister.docType, Space>>, IBqlString>.Concat<ARRegister.refNbr>), typeof (string))]
  [PX.Objects.Common.Attributes.DocumentKey(typeof (ARDocType.ListAttribute))]
  public virtual string DocumentKey { get; set; }

  /// <summary>The module from which the document originates.</summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.GL.BatchModule.FullListAttribute" />.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXDefault("AR")]
  [PXUIField]
  [BatchModule.FullList]
  public virtual string OrigModule
  {
    get => this._OrigModule;
    set => this._OrigModule = value;
  }

  /// <summary>The date of the document.</summary>
  /// <value>
  /// Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">Business Date</see>.
  /// </value>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  /// <summary>
  /// The date of the original document (e.g. the one reversed by this document).
  /// </summary>
  [PXDBDate]
  public virtual DateTime? OrigDocDate
  {
    get => this._OrigDocDate;
    set => this._OrigDocDate = value;
  }

  /// <summary>The due date of the document.</summary>
  [PXDBDate]
  [PXUIField]
  public virtual DateTime? DueDate
  {
    get => this._DueDate;
    set => this._DueDate = value;
  }

  /// <summary>
  /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
  /// </summary>
  /// <value>
  /// Determined by the <see cref="P:PX.Objects.AR.ARRegister.DocDate">date of the document</see>. Unlike <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" />
  /// the value of this field can't be overriden by user.
  /// </value>
  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  /// <summary>
  /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
  /// </summary>
  /// <value>
  /// Defaults to the period, to which the <see cref="P:PX.Objects.AR.ARRegister.DocDate" /> belongs, but can be overriden by user.
  /// </value>
  [AROpenPeriod(typeof (ARRegister.docDate), typeof (ARRegister.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (ARRegister.tranPeriodID), IsHeader = true)]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.Customer" /> record associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [CustomerActive]
  [PXDefault]
  [PXForeignReference(typeof (Field<ARRegister.customerID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CR.Location" /> of the Customer.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.CR.BAccount.DefLocationID">Default Location</see> of the <see cref="P:PX.Objects.AR.ARRegister.CustomerID">Customer</see> if it is specified,
  /// or to the first found <see cref="T:PX.Objects.CR.Location" />, associated with the Customer.
  /// Corresponds to the <see cref="P:PX.Objects.CR.Location.LocationID" /> field.
  /// </value>
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Optional<ARRegister.customerID>>, And<MatchWithBranch<PX.Objects.CR.Location.cBranchID>>>))]
  [PXDefault(typeof (Coalesce<Search2<BAccountR.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>>, Where<BAccountR.bAccountID, Equal<Current<ARRegister.customerID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>, Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<ARRegister.customerID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>>))]
  [PXForeignReference(typeof (CompositeKey<Field<ARRegister.customerID>.IsRelatedTo<PX.Objects.CR.Location.bAccountID>, Field<ARRegister.customerLocationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>>))]
  public virtual int? CustomerLocationID
  {
    get => this._CustomerLocationID;
    set => this._CustomerLocationID = value;
  }

  /// <summary>
  /// The code of the <see cref="T:PX.Objects.CM.Extensions.Currency" /> of the document.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// Corresponds to the <see cref="!:Currency.CuryID" /> field.
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
  /// The identifier of the <see cref="T:PX.Objects.GL.Account">AR account</see> to which the document should be posted.
  /// The Cash account and Year-to-Date Net Income account cannot be selected as the value of this field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(typeof (ARRegister.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AR Account", ControlAccountForModule = "AR")]
  public virtual int? ARAccountID
  {
    get => this._ARAccountID;
    set => this._ARAccountID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Sub">subaccount</see> to which the document should be posted.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (ARRegister.aRAccountID))]
  public virtual int? ARSubID
  {
    get => this._ARSubID;
    set => this._ARSubID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Account">account</see> to which the Tax and Taxable amounts
  /// should be posted when the feature <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAR" /> is activated.
  /// The Cash account and Year-to-Date Net Income account cannot be selected as the value of this field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(typeof (ARRegister.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "Prepayment Account", ControlAccountForModule = "AR")]
  public virtual int? PrepaymentAccountID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Sub">subaccount</see> to which the Tax and Taxable amounts
  /// should be posted when the feature <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAR" /> is activated.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (ARRegister.prepaymentAccountID))]
  public virtual int? PrepaymentSubID { get; set; }

  /// <summary>
  /// The counter of the document lines, which is used <i>internally</i> to assign
  /// <see cref="P:PX.Objects.AR.ARTran.LineNbr">numbers</see> to newly created <see cref="T:PX.Objects.AR.ARTran">lines</see>.
  /// We do not recommended that you rely on this field to determine the exact number of lines,
  /// which might not be reflected by the value of this field under various conditions.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  /// <summary>
  /// The counter of the document applications, which is used <i>internally</i> to assign
  /// <see cref="P:PX.Objects.AR.ARAdjust.AdjNbr">numbers</see> to newly created <see cref="T:PX.Objects.AR.ARAdjust">lines</see>.
  /// The value is used to determine old and new applications.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? AdjCntr
  {
    get => this._AdjCntr;
    set => this._AdjCntr = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? DRSchedCntr { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">CurrencyInfo</see> object associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:CurrencyInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>
  /// The amount of the document.
  /// Given in the <see cref="P:PX.Objects.AR.ARRegister.CuryID">currency of the document</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.origDocAmt))]
  [PXUIField]
  public virtual Decimal? CuryOrigDocAmt
  {
    get => this._CuryOrigDocAmt;
    set => this._CuryOrigDocAmt = value;
  }

  /// <summary>
  /// The amount of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBBaseCury(typeof (ARRegister.branchID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigDocAmt
  {
    get => this._OrigDocAmt;
    set => this._OrigDocAmt = value;
  }

  /// <summary>
  /// The open balance of the document.
  /// Given in the <see cref="P:PX.Objects.AR.ARRegister.CuryID">currency of the document</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.docBal), BaseCalc = false)]
  [DynamicLabel(typeof (BqlOperand<labelUnpaidBalance, IBqlString>.When<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.docType, Equal<ARDocType.prepaymentInvoice>>>>, And<BqlOperand<ARRegister.status, IBqlString>.IsNotEqual<ARDocStatus.unapplied>>>, And<BqlOperand<ARRegister.status, IBqlString>.IsNotEqual<ARDocStatus.closed>>>, And<BqlOperand<ARRegister.status, IBqlString>.IsNotEqual<ARDocStatus.voided>>>>.And<BqlOperand<ARRegister.status, IBqlString>.IsNotEqual<ARDocStatus.reserved>>>.Else<labelBalance>))]
  [PXUIField]
  public virtual Decimal? CuryDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  /// <summary>
  /// The open balance of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBBaseCury(typeof (ARRegister.branchID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  /// <summary>
  /// The entered in migration mode balance of the document.
  /// Given in the <see cref="P:PX.Objects.AR.ARRegister.CuryID">currency of the document</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.initDocBal))]
  [PXUIField]
  public virtual Decimal? CuryInitDocBal { get; set; }

  /// <summary>
  /// The entered in migration mode balance of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? InitDocBal { get; set; }

  [PXDBCalced(typeof (ARRegister.curyInitDocBal), typeof (Decimal))]
  [PXCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.initDocBal), BaseCalc = false)]
  [PXUIField]
  public virtual Decimal? DisplayCuryInitDocBal { get; set; }

  /// <summary>
  /// The cash discount entered for the document.
  /// Given in the <see cref="P:PX.Objects.AR.ARRegister.CuryID">currency of the document</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.origDiscAmt))]
  [PXUIField]
  public virtual Decimal? CuryOrigDiscAmt
  {
    get => this._CuryOrigDiscAmt;
    set => this._CuryOrigDiscAmt = value;
  }

  /// <summary>
  /// The cash discount entered for the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigDiscAmt
  {
    get => this._OrigDiscAmt;
    set => this._OrigDiscAmt = value;
  }

  /// <summary>
  /// The <see cref="P:PX.Objects.AR.ARAdjust.CuryAdjdDiscAmt">cash discount amount</see> actually applied to the document.
  /// Given in the <see cref="P:PX.Objects.AR.ARRegister.CuryID">currency of the document</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.discTaken))]
  public virtual Decimal? CuryDiscTaken
  {
    get => this._CuryDiscTaken;
    set => this._CuryDiscTaken = value;
  }

  /// <summary>
  /// The <see cref="P:PX.Objects.AR.ARAdjust.CuryAdjdDiscAmt">cash discount amount</see> actually applied to the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscTaken
  {
    get => this._DiscTaken;
    set => this._DiscTaken = value;
  }

  /// <summary>
  /// The cash discount balance of the document.
  /// Given in the <see cref="P:PX.Objects.AR.ARRegister.CuryID">currency of the document</see>.
  /// </summary>
  [PXUIField]
  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.discBal), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscBal
  {
    get => this._CuryDiscBal;
    set => this._CuryDiscBal = value;
  }

  /// <summary>
  /// The cash discount balance of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscBal
  {
    get => this._DiscBal;
    set => this._DiscBal = value;
  }

  /// <summary>
  /// The <see cref="P:PX.Objects.AR.ARInvoiceDiscountDetail.DiscountAmt">document discount total</see> (without group discounts).
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DocDisc
  {
    get => this._DocDisc;
    set => this._DocDisc = value;
  }

  /// <summary>
  /// The <see cref="P:PX.Objects.AR.ARInvoiceDiscountDetail.DiscountAmt">document discount total</see> (without group discounts).
  /// Given in the <see cref="P:PX.Objects.AR.ARRegister.CuryID">currency of the document</see>.
  /// </summary>
  [PXCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.docDisc))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Document Discount", Enabled = true)]
  public virtual Decimal? CuryDocDisc
  {
    get => this._CuryDocDisc;
    set => this._CuryDocDisc = value;
  }

  /// <summary>
  /// The total of all finance charges applied to the document.
  /// Given in the <see cref="P:PX.Objects.AR.ARRegister.CuryID">currency of the document</see>.
  /// </summary>
  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.chargeAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryChargeAmt
  {
    get => this._CuryChargeAmt;
    set => this._CuryChargeAmt = value;
  }

  /// <summary>
  /// The total of all finance charges applied to the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ChargeAmt
  {
    get => this._ChargeAmt;
    set => this._ChargeAmt = value;
  }

  /// <summary>The description of the document.</summary>
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXUIField]
  public virtual string DocDesc
  {
    get => this._DocDesc;
    set => this._DocDesc = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (Search<PX.Objects.CR.Location.cTaxCalcMode, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ARRegister.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<ARRegister.customerLocationID>>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

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

  [PXDBTimestamp(RecordComesFirst = true)]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  /// <summary>
  /// Reserved for internal use.
  /// The read-only class of the document determined by the <see cref="P:PX.Objects.AR.ARRegister.DocType" />.
  /// Affects the way the document is posted to the General Ledger.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.GLTran.TranClass" /> field.
  /// </value>
  [PXString(1, IsFixed = true)]
  public virtual string DocClass
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARRegister.docType)})] get
    {
      return ARDocType.DocClass(this._DocType);
    }
    set
    {
    }
  }

  /// <summary>
  /// The number of the <see cref="T:PX.Objects.GL.Batch" /> created from the document on release.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PX.Objects.GL.BatchNbr(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAR>>>), IsMigratedRecordField = typeof (ARRegister.isMigratedRecord))]
  public virtual string BatchNbr { get; set; }

  /// <summary>
  /// The batch sequence number.
  /// The field is not used.
  /// </summary>
  [PXDBShort]
  [PXDefault(0)]
  public virtual short? BatchSeq
  {
    get => this._BatchSeq;
    set => this._BatchSeq = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the document has been released.
  /// </summary>
  [PX.Objects.GL.Released(PreventDeletingReleased = true)]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>
  /// When set, on persist checks, that the document has the corresponded <see cref="P:PX.Objects.AR.ARRegister.Released" /> original value.
  /// When not set, on persist checks, that <see cref="P:PX.Objects.AR.ARRegister.Released" /> value is not changed.
  /// Throws an error otherwise.
  /// </summary>
  [PXDBRestrictionBool(typeof (ARRegister.released))]
  public virtual bool? ReleasedToVerify { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the document is open.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? OpenDoc
  {
    get => this._OpenDoc;
    set => this._OpenDoc = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the document is on hold and thus cannot be released.
  /// </summary>
  [PXDBBool]
  [PXUIField]
  [PXDefault(true, typeof (ARSetup.holdEntry))]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the document is part of a <c>Schedule</c> and serves as a template for generating other documents according to it.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Scheduled
  {
    get => this._Scheduled;
    set => this._Scheduled = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the document is generating by a <c>Scedule</c> process.
  /// </summary>
  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? FromSchedule { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that the document has been voided.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  /// <summary>
  /// When <c>true</c>, indicates that the document can be voided only in full and
  /// it is not allow to delete reversing applications partially.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXFormula(typeof (IIf<Where<IsSelfVoiding<ARRegister.docType>>, True, False>))]
  public virtual bool? SelfVoidingDoc { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
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

  /// <summary>The date of the last application.</summary>
  [PXDBDate]
  [PXUIField]
  public virtual DateTime? ClosedDate { get; set; }

  /// <summary>!REV!</summary>
  [PXDBGuid(false)]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  /// <summary>
  /// The <see cref="!:FinancialPeriod">Financial Period</see>, in which the document was closed.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID" /> field.
  /// </value>
  [PX.Objects.GL.FinPeriodID(null, typeof (ARRegister.branchID), null, null, null, null, true, false, null, typeof (ARRegister.closedTranPeriodID), null, true, true)]
  [PXUIField]
  public virtual string ClosedFinPeriodID
  {
    get => this._ClosedFinPeriodID;
    set => this._ClosedFinPeriodID = value;
  }

  /// <summary>
  /// The <see cref="!:FinancialPeriod">Financial Period</see>, in which the document was closed.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARRegister.TranPeriodID" /> field.
  /// </value>
  [PeriodID(null, null, null, true)]
  [PXUIField]
  public virtual string ClosedTranPeriodID
  {
    get => this._ClosedTranPeriodID;
    set => this._ClosedTranPeriodID = value;
  }

  /// <summary>
  /// Realized Gain or Loss amount associated with the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RGOLAmt
  {
    get => this._RGOLAmt;
    set => this._RGOLAmt = value;
  }

  /// <summary>
  /// The difference between the original amount of the document and the rounded amount.
  /// Given in the <see cref="P:PX.Objects.AR.ARRegister.CuryID">currency of the document</see>.
  /// Applicable only if <see cref="P:PX.Objects.CS.FeaturesSet.InvoiceRounding">Invoice Rounding</see> feature is enabled.
  /// </summary>
  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.roundDiff), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? CuryRoundDiff { get; set; }

  /// <summary>
  /// The difference between the original amount of the document and the rounded amount,
  /// in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// The field is used only if the <see cref="P:PX.Objects.CS.FeaturesSet.InvoiceRounding">Invoice Rounding</see> feature is enabled.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? RoundDiff { get; set; }

  /// <summary>
  /// A read-only field, which indicates (if set to <c>true</c>) that the document is payable.
  /// The value of the field depends on the<see cref="P:PX.Objects.AR.ARRegister.DocType" /> field and is opposite to the value of the <see cref="P:PX.Objects.AR.ARRegister.Paying" /> field.
  /// </summary>
  public virtual bool? Payable
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARRegister.docType), typeof (ARRegister.pendingPayment)})] get
    {
      return !(this._DocType == "PPI") ? ARDocType.Payable(this._DocType) : new bool?(this.PendingPayment.GetValueOrDefault());
    }
    set
    {
    }
  }

  /// <summary>
  /// Read-only field indicating whether the document is paying.
  /// Opposite of the <see cref="P:PX.Objects.AR.ARRegister.Payable" /> field.
  /// </summary>
  public virtual bool? Paying
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARRegister.docType), typeof (ARRegister.pendingPayment)})] get
    {
      int num;
      if (!(this._DocType == "PPI"))
      {
        bool? nullable = ARDocType.Payable(this._DocType);
        bool flag = false;
        num = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
      }
      else
        num = !this.PendingPayment.GetValueOrDefault() ? 1 : 0;
      return new bool?(num != 0);
    }
    set
    {
    }
  }

  /// <summary>
  /// Read-only field determining the sort order for AR documents based on the <see cref="P:PX.Objects.AR.ARRegister.DocType" /> field.
  /// </summary>
  public virtual short? SortOrder
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARRegister.docType)})] get
    {
      return ARDocType.SortOrder(this._DocType);
    }
    set
    {
    }
  }

  /// <summary>
  /// Read-only field indicating the sign of the document's impact on AR balance .
  /// Depends solely on the <see cref="P:PX.Objects.AR.ARRegister.DocType" /> field.
  /// </summary>
  /// <value>
  /// Possible values are: <c>1</c>, <c>-1</c> or <c>0</c>.
  /// </value>
  public virtual Decimal? SignBalance
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARRegister.docType)})] get
    {
      return ARDocType.SignBalance(this._DocType);
    }
    set
    {
    }
  }

  /// <summary>
  /// Read-only field indicating the sign of the document amount.
  /// Depends solely on the <see cref="P:PX.Objects.AR.ARRegister.DocType" /> field.
  /// </summary>
  /// <value>
  /// Possible values are: <c>1</c>, <c>-1</c> or <c>0</c>.
  /// </value>
  public virtual Decimal? SignAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARRegister.docType)})] get
    {
      return ARDocType.SignAmount(this._DocType);
    }
    set
    {
    }
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Schedule" />, associated with the document.
  /// In case <see cref="P:PX.Objects.AR.ARRegister.Scheduled" /> is <c>true</c>, the field points to the Schedule, to which the document belongs as a template.
  /// Otherwise, the field points to the Schedule, from which this document was generated, if any.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Schedule.ScheduleID" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  public virtual string ScheduleID
  {
    get => this._ScheduleID;
    set => this._ScheduleID = value;
  }

  /// <summary>
  /// Implementation specific reference number of the document.
  /// This field is neither filled nor used by the core Acumatica itself, but may be utilized by customizations or extensions.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  public virtual string ImpRefNbr
  {
    get => this._ImpRefNbr;
    set => this._ImpRefNbr = value;
  }

  /// <summary>
  /// The date of the <see cref="T:PX.Objects.AR.ARStatement">Customer Statement</see>, in which the document is reported.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARStatement.StatementDate" /> field.
  /// </value>
  [PXDBDate]
  public virtual DateTime? StatementDate
  {
    get => this._StatementDate;
    set => this._StatementDate = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.CustSalesPeople">salesperson</see> to whom the document belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.CustSalesPeople.SalesPersonID" /> field.
  /// </value>
  [SalesPerson]
  [PXDefault(typeof (Search<CustDefSalesPeople.salesPersonID, Where<CustDefSalesPeople.bAccountID, Equal<Current<ARRegister.customerID>>, And<CustDefSalesPeople.locationID, Equal<Current<ARRegister.customerLocationID>>, And<CustDefSalesPeople.isDefault, Equal<True>>>>>))]
  [PXForeignReference(typeof (Field<ARRegister.salesPersonID>.IsRelatedTo<SalesPerson.salesPersonID>))]
  public virtual int? SalesPersonID
  {
    get => this._SalesPersonID;
    set => this._SalesPersonID = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Disable Automatic Tax Calculation", Enabled = false)]
  public virtual bool? DisableAutomaticTaxCalculation { get; set; }

  /// <summary>
  /// When <c>true</c>, indicates that the amount of tax calculated with the external Tax Engine(Avalara) is up to date.
  /// If this field equals <c>false</c>, the document was updated since last synchronization with the Tax Engine
  /// and taxes might need recalculation.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Is Up to Date", Enabled = false)]
  public virtual bool? IsTaxValid { get; set; }

  /// <summary>
  /// When <c>true</c>, indicates that the tax information was successfully commited to the external Tax Engine(Avalara).
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Is Posted/Committed to External Tax Engine (Avalara)", Enabled = false)]
  public virtual bool? IsTaxPosted { get; set; }

  /// <summary>
  /// Indicates whether the tax information related to the document was saved to the external Tax Engine (Avalara).
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Is Saved in External Tax Engine (Avalara)", Enabled = false)]
  public virtual bool? IsTaxSaved { get; set; }

  /// <summary>
  /// Get or set NonTaxable that mark current document does not impose sales taxes.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Non-Taxable", Enabled = false)]
  public virtual bool? NonTaxable { get; set; }

  /// <summary>The type of the original (source) document.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARRegister.DocType" /> field.
  /// </value>
  [PXDBString(3, IsFixed = true)]
  [ARDocType.List]
  [PXUIField(DisplayName = "Orig. Doc. Type")]
  public virtual string OrigDocType
  {
    get => this._OrigDocType;
    set => this._OrigDocType = value;
  }

  /// <summary>
  /// The reference number of the original (source) document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARRegister.RefNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField]
  public virtual string OrigRefNbr { get; set; }

  /// <summary>
  /// The status of the document.
  /// The value of the field is determined by the values of the status flags,
  /// such as <see cref="P:PX.Objects.AR.ARRegister.Hold" />, <see cref="P:PX.Objects.AR.ARRegister.Released" />, <see cref="P:PX.Objects.AR.ARRegister.Voided" />, <see cref="P:PX.Objects.AR.ARRegister.Scheduled" />.
  /// </summary>
  /// <value>
  /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocStatus.ListAttribute" />.
  /// Defaults to <see cref="F:PX.Objects.AR.ARDocStatus.Hold" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField]
  [ARDocStatus.List]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>
  /// The discounted amount of the document.
  /// Given in the <see cref="P:PX.Objects.AR.ARRegister.CuryID"> currency of the document</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.discountedDocTotal))]
  [PXUIField]
  public virtual Decimal? CuryDiscountedDocTotal
  {
    get => this._CuryDiscountedDocTotal;
    set => this._CuryDiscountedDocTotal = value;
  }

  /// <summary>
  /// The discounted amount of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscountedDocTotal
  {
    get => this._DiscountedDocTotal;
    set => this._DiscountedDocTotal = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.discountedTaxableTotal))]
  [PXUIField]
  public virtual Decimal? CuryDiscountedTaxableTotal
  {
    get => this._CuryDiscountedTaxableTotal;
    set => this._CuryDiscountedTaxableTotal = value;
  }

  /// <summary>
  /// The total taxable amount reduced on early payment, according to cash discount.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscountedTaxableTotal
  {
    get => this._DiscountedTaxableTotal;
    set => this._DiscountedTaxableTotal = value;
  }

  /// <summary>
  /// The total tax amount reduced on early payment, according to cash discount.
  /// Given in the <see cref="P:PX.Objects.AR.ARRegister.CuryID"> currency of the document</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.discountedPrice))]
  [PXUIField]
  public virtual Decimal? CuryDiscountedPrice
  {
    get => this._CuryDiscountedPrice;
    set => this._CuryDiscountedPrice = value;
  }

  /// <summary>
  /// The total tax amount reduced on early payment, according to cash discount.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscountedPrice
  {
    get => this._DiscountedPrice;
    set => this._DiscountedPrice = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that the document has the taxes that reduce cash discount taxable amount on early payment.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? HasPPDTaxes
  {
    get => this._HasPPDTaxes;
    set => this._HasPPDTaxes = value;
  }

  /// <summary>
  /// If set to <c>true</c>, indicates that the document has been fully paid and
  /// to close the document, you need to apply the cash discount by generating a
  /// credit memo on the Generate AR Tax Adjustments (AR504500) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PendingPPD
  {
    get => this._PendingPPD;
    set => this._PendingPPD = value;
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// in migration mode without affecting GL module.
  /// </summary>
  [MigratedRecord(typeof (ARSetup.migrationMode))]
  public virtual bool? IsMigratedRecord { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// with activated <see cref="P:PX.Objects.CS.FeaturesSet.PaymentsByLines" /> feature and
  /// such document allow payments by lines.
  /// </summary>
  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? PaymentsByLinesAllowed { get; set; }

  [Owner]
  public virtual int? ApproverID { get; set; }

  [PXDBInt]
  [PXCompanyTreeSelector]
  [PXUIField(DisplayName = "Approval Workgroup ID", Enabled = false)]
  public virtual int? ApproverWorkgroupID { get; set; }

  int? IAssign.WorkgroupID
  {
    get => this.ApproverWorkgroupID;
    set => this.ApproverWorkgroupID = value;
  }

  int? IAssign.OwnerID
  {
    get => this.ApproverID;
    set => this.ApproverID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Approved { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Rejected { get; set; }

  /// <summary>
  /// Indicates that the current document should be excluded from the
  /// approval process. Maintenance of this property is on graph level.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? DontApprove { get; set; }

  [Account(typeof (ARRegister.branchID), DisplayName = "Retainage Receivable Account", DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "AR")]
  [PXDefault]
  public virtual int? RetainageAcctID { get; set; }

  [SubAccount(typeof (ARRegister.retainageAcctID), typeof (ARRegister.branchID), true, DisplayName = "Retainage Receivable Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXDefault]
  public virtual int? RetainageSubID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Apply Retainage", FieldClass = "Retainage")]
  [PXDefault(false)]
  public virtual bool? RetainageApply { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Retainage Document", Enabled = false, FieldClass = "Retainage")]
  [PXDefault(false)]
  public virtual bool? IsRetainageDocument { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Retainage Reversing", Enabled = false, FieldClass = "Retainage")]
  [PXDefault(false)]
  public virtual bool? IsRetainageReversing { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Default Retainage Percent", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DefRetainagePct { get; set; }

  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.lineRetainageTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineRetainageTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineRetainageTotal { get; set; }

  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.retainageTotal))]
  [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainageTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
  public virtual Decimal? RetainageTotal { get; set; }

  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.retainageUnreleasedAmt))]
  [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
  public virtual Decimal? RetainageUnreleasedAmt { get; set; }

  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.retainageReleased))]
  [PXUIField(DisplayName = "Released Retainage", FieldClass = "Retainage")]
  [PXFormula(typeof (Switch<Case<Where<ARRegister.isRetainageReversing, Equal<True>>, decimal0>, Sub<ARRegister.curyRetainageTotal, ARRegister.curyRetainageUnreleasedAmt>>))]
  public virtual Decimal? CuryRetainageReleased { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Released Retainage", FieldClass = "Retainage")]
  public virtual Decimal? RetainageReleased { get; set; }

  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.retainedTaxTotal))]
  [PXUIField(DisplayName = "Tax on Retainage", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainedTaxTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainedTaxTotal { get; set; }

  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.retainedDiscTotal))]
  [PXUIField(DisplayName = "Discount on Retainage", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainedDiscTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainedDiscTotal { get; set; }

  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.retainageUnpaidTotal))]
  [PXUIField(DisplayName = "Unpaid Retainage", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryRetainageUnpaidTotal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageUnpaidTotal { get; set; }

  [PXCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.retainagePaidTotal))]
  [PXUIField(DisplayName = "Paid/Adjusted Retainage", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Sub<Add<ARRegister.curyRetainageUnreleasedAmt, ARRegister.curyRetainageReleased>, ARRegister.curyRetainageUnpaidTotal>))]
  public virtual Decimal? CuryRetainagePaidTotal { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Sub<Add<ARRegister.retainageUnreleasedAmt, ARRegister.retainageReleased>, ARRegister.retainageUnpaidTotal>))]
  public virtual Decimal? RetainagePaidTotal { get; set; }

  [PXCury(typeof (ARRegister.curyID))]
  [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Add<ARRegister.curyOrigDocAmt, ARRegister.curyRetainageTotal>))]
  public virtual Decimal? CuryOrigDocAmtWithRetainageTotal { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
  [PXFormula(typeof (Add<ARRegister.origDocAmt, ARRegister.retainageTotal>))]
  public virtual Decimal? OrigDocAmtWithRetainageTotal { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the invoice is a cancellation invoice (credit memo).
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCancellation { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the invoice is a correction invoice.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Correction Inv.", Visible = false, Enabled = false)]
  public virtual bool? IsCorrection { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that Cancel or Correct action was applied to the invoice.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsUnderCorrection { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the invoice was canceled or corrected.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Canceled { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PendingProcessing { get; set; }

  [PXDBString(80 /*0x50*/)]
  public virtual string ExternalRef { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the prepayment ready for payment applicaton
  /// when the feature <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAR" /> is activated.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PendingPayment { get; set; }

  /// <summary>
  /// Indicates that events will be invoke in the ARDocumentRelease.ProcessPostponedFlags() method
  /// for <see cref="T:PX.Objects.AR.ARRegister.pendingPayment" /> field.
  /// Uses when the feature <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAR" /> is activated.
  /// </summary>
  [PXBool]
  public virtual bool? PostponePendingPaymentFlag { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that the document should not be sent to the <see cref="P:PX.Objects.AR.ARRegister.CustomerID">Customer</see>
  /// as a printed document, and thus the system should not include it in the list of documents available for mass-printing.
  /// </summary>
  /// <value>
  /// Defaults to the value of the <see cref="P:PX.Objects.AR.Customer.PrintInvoices" /> setting of the <see cref="P:PX.Objects.AR.ARRegister.CustomerID">Customer</see>.
  /// </value>
  [PXDBBool]
  [FormulaDefault(typeof (IIf<Where<ARRegister.isMigratedRecord, Equal<True>, Or<Current<Customer.printInvoices>, Equal<True>>>, False, True>))]
  [PXDefault]
  [PXUIField(DisplayName = "Don't Print")]
  public virtual bool? DontPrint { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the document has been printed.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Printed", Enabled = false)]
  public virtual bool? Printed { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that the document should not be sent to the <see cref="P:PX.Objects.AR.ARRegister.CustomerID">Customer</see>
  /// by email, and thus the system should not include it in the list of documents available for mass-emailing.
  /// </summary>
  /// <value>
  /// Defaults to the value of the <see cref="P:PX.Objects.AR.Customer.MailInvoices" /> setting of the <see cref="P:PX.Objects.AR.ARRegister.CustomerID">Customer</see>.
  /// </value>
  [PXDBBool]
  [FormulaDefault(typeof (IIf<Where<ARRegister.isMigratedRecord, Equal<True>, Or<Current<Customer.mailInvoices>, Equal<True>>>, False, True>))]
  [PXDefault]
  [PXUIField(DisplayName = "Don't Email")]
  public virtual bool? DontEmail { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the document has been emailed to the <see cref="P:PX.Objects.AR.ARInvoice.CustomerID">customer</see>.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Emailed", Enabled = false)]
  public virtual bool? Emailed { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the document awaits printing.
  /// The field is used in automation steps for the Invoices and Memos (SO303000) form and thus defines
  /// participates in determining whether the document is available for processing
  /// on the Process Invoices and Memos (SO505000) form.
  /// </summary>
  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<ARRegister.dontPrint, Equal<False>, And<ARRegister.printed, Equal<False>>>, True>, False>), typeof (bool))]
  public virtual bool? PrintInvoice
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARRegister.dontPrint), typeof (ARRegister.printed)})] get
    {
      int num;
      if (!this.DontPrint.GetValueOrDefault())
      {
        if (this.Printed.HasValue)
        {
          bool? printed = this.Printed;
          bool flag = false;
          num = printed.GetValueOrDefault() == flag & printed.HasValue ? 1 : 0;
        }
        else
          num = 1;
      }
      else
        num = 0;
      return new bool?(num != 0);
    }
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the document awaits emailing.
  /// The field is used in automation steps for the Invoices and Memos (SO303000) form and thus defines
  /// participates in determining whether the document is available for processing
  /// on the Process Invoices and Memos (SO505000) form.
  /// </summary>
  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<ARRegister.dontEmail, Equal<False>, And<ARRegister.emailed, Equal<False>>>, True>, False>), typeof (bool))]
  public virtual bool? EmailInvoice
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARRegister.dontEmail), typeof (ARRegister.emailed)})] get
    {
      int num;
      if (!this.DontEmail.GetValueOrDefault())
      {
        if (this.Emailed.HasValue)
        {
          bool? emailed = this.Emailed;
          bool flag = false;
          num = emailed.GetValueOrDefault() == flag & emailed.HasValue ? 1 : 0;
        }
        else
          num = 1;
      }
      else
        num = 0;
      return new bool?(num != 0);
    }
  }

  internal string WarningMessage { get; set; }

  public class Events : PXEntityEventBase<ARRegister>.Container<ARRegister.Events>
  {
    public PXEntityEvent<ARRegister, PX.Objects.GL.Schedule> ConfirmSchedule;
    public PXEntityEvent<ARRegister, PX.Objects.GL.Schedule> VoidSchedule;
  }

  /// <exclude />
  public class PK : PrimaryKeyOf<ARRegister>.By<ARRegister.docType, ARRegister.refNbr>
  {
    public static ARRegister Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (ARRegister) PrimaryKeyOf<ARRegister>.By<ARRegister.docType, ARRegister.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARRegister>.By<ARRegister.branchID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARRegister>.By<ARRegister.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<ARRegister>.By<ARRegister.customerID, ARRegister.customerLocationID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARRegister>.By<ARRegister.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<ARRegister>.By<ARRegister.curyID>
    {
    }

    public class ARAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARRegister>.By<ARRegister.aRAccountID>
    {
    }

    public class ARSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ARRegister>.By<ARRegister.aRSubID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<PX.Objects.GL.Schedule>.By<PX.Objects.GL.Schedule.scheduleID>.ForeignKeyOf<ARRegister>.By<ARRegister.scheduleID>
    {
    }

    public class RetainageAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARRegister>.By<ARRegister.retainageAcctID>
    {
    }

    public class RetainageSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ARRegister>.By<ARRegister.retainageSubID>
    {
    }

    public class OriginalDocument : 
      PrimaryKeyOf<ARRegister>.By<ARRegister.docType, ARRegister.refNbr>.ForeignKeyOf<ARRegister>.By<ARRegister.origDocType, ARRegister.origRefNbr>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegister.branchID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.docType>
  {
    public const int Length = 3;
  }

  public abstract class printDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.printDocType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.refNbr>
  {
  }

  public abstract class documentKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.documentKey>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.origModule>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARRegister.docDate>
  {
  }

  public abstract class origDocDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARRegister.origDocDate>
  {
  }

  public abstract class dueDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARRegister.dueDate>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.tranPeriodID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.finPeriodID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegister.customerID>
  {
  }

  public abstract class customerID_Customer_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegister.customerID_Customer_acctName>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegister.customerLocationID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.curyID>
  {
  }

  public abstract class aRAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegister.aRAccountID>
  {
  }

  public abstract class aRSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegister.aRSubID>
  {
  }

  public abstract class prepaymentAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegister.prepaymentAccountID>
  {
  }

  public abstract class prepaymentSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegister.prepaymentSubID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegister.lineCntr>
  {
  }

  public abstract class adjCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegister.adjCntr>
  {
  }

  public abstract class drSchedCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegister.drSchedCntr>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARRegister.curyInfoID>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.curyOrigDocAmt>
  {
  }

  public abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.origDocAmt>
  {
  }

  public abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.curyDocBal>
  {
  }

  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.docBal>
  {
  }

  public abstract class curyInitDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.curyInitDocBal>
  {
  }

  public abstract class initDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.initDocBal>
  {
  }

  public abstract class displayCuryInitDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.displayCuryInitDocBal>
  {
  }

  public abstract class curyOrigDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.curyOrigDiscAmt>
  {
  }

  public abstract class origDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.origDiscAmt>
  {
  }

  public abstract class curyDiscTaken : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.curyDiscTaken>
  {
  }

  public abstract class discTaken : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.discTaken>
  {
  }

  public abstract class curyDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.curyDiscBal>
  {
  }

  public abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.discBal>
  {
  }

  [Obsolete("This field is obsolete and will be removed in 2021R1.")]
  public abstract class docDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.docDisc>
  {
  }

  [Obsolete("This field is obsolete and will be removed in 2021R1.")]
  public abstract class curyDocDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.curyDocDisc>
  {
  }

  public abstract class curyChargeAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.curyChargeAmt>
  {
  }

  public abstract class chargeAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.chargeAmt>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.docDesc>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.taxCalcMode>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARRegister.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegister.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARRegister.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARRegister.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegister.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARRegister.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARRegister.Tstamp>
  {
  }

  public abstract class docClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.docClass>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.batchNbr>
  {
  }

  public abstract class batchSeq : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARRegister.batchSeq>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.released>
  {
  }

  /// <exclude />
  public abstract class releasedToVerify : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.releasedToVerify>
  {
  }

  public abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.openDoc>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.hold>
  {
  }

  public abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.scheduled>
  {
  }

  public abstract class fromSchedule : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.fromSchedule>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.voided>
  {
  }

  public abstract class selfVoidingDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.selfVoidingDoc>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARRegister.noteID>
  {
  }

  public abstract class closedDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARRegister.closedDate>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARRegister.refNoteID>
  {
  }

  public abstract class closedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegister.closedFinPeriodID>
  {
  }

  public abstract class closedTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARRegister.closedTranPeriodID>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.rGOLAmt>
  {
  }

  public abstract class curyRoundDiff : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.curyRoundDiff>
  {
  }

  public abstract class roundDiff : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARRegister.roundDiff>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.scheduleID>
  {
  }

  public abstract class impRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.impRefNbr>
  {
  }

  public abstract class statementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARRegister.statementDate>
  {
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegister.salesPersonID>
  {
  }

  public abstract class disableAutomaticTaxCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegister.disableAutomaticTaxCalculation>
  {
  }

  public abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.isTaxValid>
  {
  }

  public abstract class isTaxPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.isTaxPosted>
  {
  }

  public abstract class isTaxSaved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.isTaxSaved>
  {
  }

  public abstract class nonTaxable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.nonTaxable>
  {
  }

  public abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.origDocType>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.origRefNbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.status>
  {
  }

  public abstract class curyDiscountedDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.curyDiscountedDocTotal>
  {
  }

  public abstract class discountedDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.discountedDocTotal>
  {
  }

  public abstract class curyDiscountedTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.curyDiscountedTaxableTotal>
  {
  }

  public abstract class discountedTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.discountedTaxableTotal>
  {
  }

  public abstract class curyDiscountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.curyDiscountedPrice>
  {
  }

  public abstract class discountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.discountedPrice>
  {
  }

  public abstract class hasPPDTaxes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.hasPPDTaxes>
  {
  }

  public abstract class pendingPPD : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.pendingPPD>
  {
  }

  public abstract class isMigratedRecord : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.isMigratedRecord>
  {
  }

  public abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegister.paymentsByLinesAllowed>
  {
  }

  public abstract class approverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegister.approverID>
  {
  }

  public abstract class approverWorkgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARRegister.approverWorkgroupID>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.rejected>
  {
  }

  public abstract class dontApprove : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.dontApprove>
  {
  }

  public abstract class retainageAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegister.retainageAcctID>
  {
  }

  public abstract class retainageSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARRegister.retainageSubID>
  {
  }

  public abstract class retainageApply : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.retainageApply>
  {
  }

  public abstract class isRetainageDocument : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegister.isRetainageDocument>
  {
  }

  public abstract class isRetainageReversing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegister.isRetainageReversing>
  {
  }

  public abstract class defRetainagePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.defRetainagePct>
  {
  }

  public abstract class curyLineRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.curyLineRetainageTotal>
  {
  }

  public abstract class lineRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.lineRetainageTotal>
  {
  }

  public abstract class curyRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.curyRetainageTotal>
  {
  }

  public abstract class retainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.retainageTotal>
  {
  }

  public abstract class curyRetainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.curyRetainageUnreleasedAmt>
  {
  }

  public abstract class retainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.retainageUnreleasedAmt>
  {
  }

  public abstract class curyRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.curyRetainageReleased>
  {
  }

  public abstract class retainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.retainageReleased>
  {
  }

  public abstract class curyRetainedTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.curyRetainedTaxTotal>
  {
  }

  public abstract class retainedTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.retainedTaxTotal>
  {
  }

  public abstract class curyRetainedDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.curyRetainedDiscTotal>
  {
  }

  public abstract class retainedDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.retainedDiscTotal>
  {
  }

  public abstract class curyRetainageUnpaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.curyRetainageUnpaidTotal>
  {
  }

  public abstract class retainageUnpaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.retainageUnpaidTotal>
  {
  }

  public abstract class curyRetainagePaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.curyRetainagePaidTotal>
  {
  }

  public abstract class retainagePaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.retainagePaidTotal>
  {
  }

  public abstract class curyOrigDocAmtWithRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.curyOrigDocAmtWithRetainageTotal>
  {
  }

  public abstract class origDocAmtWithRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARRegister.origDocAmtWithRetainageTotal>
  {
  }

  /// <exclude />
  public abstract class isCancellation : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.isCancellation>
  {
  }

  /// <exclude />
  public abstract class isCorrection : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.isCorrection>
  {
  }

  /// <exclude />
  public abstract class isUnderCorrection : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegister.isUnderCorrection>
  {
  }

  /// <exclude />
  public abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.canceled>
  {
  }

  public abstract class pendingProcessing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegister.pendingProcessing>
  {
  }

  /// <summary>
  /// External document ID that is needed to identify document for the integrations with
  /// external systems such as Commerce Edition
  /// </summary>
  public abstract class externalRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARRegister.externalRef>
  {
  }

  public abstract class pendingPayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.pendingPayment>
  {
  }

  public abstract class postponePendingPaymentFlag : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARRegister.postponePendingPaymentFlag>
  {
  }

  public abstract class dontPrint : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.dontPrint>
  {
  }

  public abstract class printed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.printed>
  {
  }

  public abstract class dontEmail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.dontEmail>
  {
  }

  public abstract class emailed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.emailed>
  {
  }

  public abstract class printInvoice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.printInvoice>
  {
  }

  public abstract class emailInvoice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARRegister.emailInvoice>
  {
  }

  public class SetStatusAttribute : 
    PXEventSubscriberAttribute,
    IPXRowUpdatingSubscriber,
    IPXRowInsertingSubscriber
  {
    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      // ISSUE: method pointer
      sender.Graph.FieldUpdating.AddHandler(sender.GetItemType(), "hold", new PXFieldUpdating((object) this, __methodptr(\u003CCacheAttached\u003Eb__0_0)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      sender.Graph.FieldVerifying.AddHandler(sender.GetItemType(), "Status", ARRegister.SetStatusAttribute.\u003C\u003Ec.\u003C\u003E9__0_1 ?? (ARRegister.SetStatusAttribute.\u003C\u003Ec.\u003C\u003E9__0_1 = new PXFieldVerifying((object) ARRegister.SetStatusAttribute.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCacheAttached\u003Eb__0_1))));
      PXGraph.RowSelectingEvents rowSelecting = sender.Graph.RowSelecting;
      System.Type itemType = sender.GetItemType();
      ARRegister.SetStatusAttribute setStatusAttribute = this;
      // ISSUE: virtual method pointer
      PXRowSelecting pxRowSelecting = new PXRowSelecting((object) setStatusAttribute, __vmethodptr(setStatusAttribute, RowSelecting));
      rowSelecting.AddHandler(itemType, pxRowSelecting);
      // ISSUE: method pointer
      sender.Graph.RowSelected.AddHandler(sender.GetItemType(), new PXRowSelected((object) this, __methodptr(\u003CCacheAttached\u003Eb__0_2)));
    }

    protected virtual void StatusSet(PXCache cache, ARRegister item, bool? holdVal)
    {
      if (item.Canceled.GetValueOrDefault())
        item.Status = "L";
      else if (item.Voided.GetValueOrDefault())
        item.Status = "V";
      else if (item.Hold.GetValueOrDefault())
      {
        if (item.Released.GetValueOrDefault())
          item.Status = "Z";
        else
          item.Status = "H";
      }
      else if (item.Scheduled.GetValueOrDefault())
        item.Status = "S";
      else if (item.Rejected.GetValueOrDefault())
        item.Status = "J";
      else if (!item.Released.GetValueOrDefault())
      {
        if (!item.Approved.GetValueOrDefault() && !item.DontApprove.GetValueOrDefault() && (item.DocType == "RCS" || item.DocType == "REF" || item.DocType == "INV" || item.DocType == "DRM" || item.DocType == "CRM"))
          item.Status = "D";
        else if (item.PendingProcessing.GetValueOrDefault())
        {
          item.Status = "W";
        }
        else
        {
          bool? pendingProcessing = item.PendingProcessing;
          bool flag = false;
          if (!(pendingProcessing.GetValueOrDefault() == flag & pendingProcessing.HasValue))
            return;
          item.Status = "B";
        }
      }
      else if (item.OpenDoc.GetValueOrDefault())
      {
        item.Status = item.DocType == "PPI" ? "U" : "N";
      }
      else
      {
        bool? openDoc = item.OpenDoc;
        bool flag = false;
        if (!(openDoc.GetValueOrDefault() == flag & openDoc.HasValue))
          return;
        item.Status = "C";
      }
    }

    public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
    {
      ARRegister row = (ARRegister) e.Row;
      if (row == null)
        return;
      this.StatusSet(sender, row, row.Hold);
    }

    public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
    {
      ARRegister row = (ARRegister) e.Row;
      this.StatusSet(sender, row, row.Hold);
    }

    public virtual void RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
    {
      ARRegister newRow = (ARRegister) e.NewRow;
      this.StatusSet(sender, newRow, newRow.Hold);
    }
  }
}
