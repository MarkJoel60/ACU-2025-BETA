// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APRegister
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
using PX.Objects.AP.Standalone;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.MigrationMode;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.TX;
using PX.TM;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// Primary DAC for the Accounts Payable documents.
/// Includes fields common to all types of AP documents.
/// </summary>
[DebuggerDisplay("{GetType()}: DocType = {DocType}, RefNbr = {RefNbr}, tstamp = {PX.Data.PXDBTimestampAttribute.ToString(tstamp)}")]
[PXCacheName("Document")]
[PXPrimaryGraph(new System.Type[] {typeof (APQuickCheckEntry), typeof (TXInvoiceEntry), typeof (APInvoiceEntry), typeof (APPaymentEntry)}, new System.Type[] {typeof (Select<APQuickCheck, Where<APQuickCheck.docType, Equal<Current<APRegister.docType>>, And<APQuickCheck.refNbr, Equal<Current<APRegister.refNbr>>>>>), typeof (Select<APInvoice, Where<APInvoice.docType, Equal<Current<APRegister.docType>>, And<APInvoice.refNbr, Equal<Current<APRegister.refNbr>>, PX.Data.And<Where<APInvoice.released, Equal<False>, And<APRegister.origModule, Equal<BatchModule.moduleTX>>>>>>>), typeof (Select<APInvoice, Where<APInvoice.docType, Equal<Current<APRegister.docType>>, And<APInvoice.refNbr, Equal<Current<APRegister.refNbr>>>>>), typeof (Select<APPayment, Where<APPayment.docType, Equal<Current<APRegister.docType>>, And<APPayment.refNbr, Equal<Current<APRegister.refNbr>>>>>)})]
[PXGroupMask(typeof (InnerJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APRegister.vendorID>, PX.Data.And<Match<Vendor, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class APRegister : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  PX.Objects.CM.IRegister,
  IDocumentKey,
  IBalance,
  IProjectHeader,
  INotable
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _HiddenKey;
  protected int? _BranchID;
  protected string _DocType;
  protected string _RefNbr;
  protected string _OrigModule;
  protected System.DateTime? _DocDate;
  protected System.DateTime? _OrigDocDate;
  protected string _TranPeriodID;
  protected string _FinPeriodID;
  protected int? _VendorLocationID;
  protected string _CuryID;
  protected int? _APAccountID;
  protected int? _APSubID;
  protected int? _LineCntr;
  protected long? _CuryInfoID;
  protected Decimal? _CuryOrigDocAmt;
  protected Decimal? _OrigDocAmt;
  protected Decimal? _CuryDocBal;
  protected Decimal? _DocBal;
  protected Decimal? _DiscTot;
  protected Decimal? _CuryDiscTot;
  protected Decimal? _DocDisc;
  protected Decimal? _CuryDocDisc;
  protected Decimal? _CuryOrigDiscAmt;
  protected Decimal? _OrigDiscAmt;
  protected Decimal? _CuryDiscTaken;
  protected Decimal? _DiscTaken;
  protected Decimal? _CuryDiscBal;
  protected Decimal? _DiscBal;
  protected Decimal? _CuryOrigWhTaxAmt;
  protected Decimal? _OrigWhTaxAmt;
  protected Decimal? _CuryWhTaxBal;
  protected Decimal? _WhTaxBal;
  protected Decimal? _CuryTaxWheld;
  protected Decimal? _TaxWheld;
  protected Decimal? _CuryChargeAmt;
  protected Decimal? _ChargeAmt;
  protected string _DocDesc;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;
  protected string _PrebookBatchNbr;
  protected string _VoidBatchNbr;
  protected bool? _Released;
  protected bool? _OpenDoc;
  protected bool? _Hold;
  protected bool? _Scheduled;
  protected bool? _Voided;
  protected bool? _Printed;
  protected bool? _Prebooked;
  protected Guid? _NoteID;
  protected Guid? _RefNoteID;
  protected string _ClosedFinPeriodID;
  protected string _ClosedTranPeriodID;
  protected Decimal? _RGOLAmt;
  protected string _Status;
  protected string _ScheduleID;
  protected string _ImpRefNbr;
  protected string _OrigDocType;
  protected string _TaxCalcMode;
  protected Decimal? _DiscountedDocTotal;
  protected Decimal? _CuryDiscountedTaxableTotal;
  protected Decimal? _DiscountedTaxableTotal;
  protected Decimal? _CuryDiscountedPrice;
  protected Decimal? _DiscountedPrice;
  protected bool? _HasPPDTaxes;
  protected bool? _PendingPPD;

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
  /// If not null, this field indicates that the document represents a payment by a separate check.
  /// In this case the payment cannot be combined with other payments to the vendor.
  /// Applicable only in case <see cref="!:PX.Objects.CR.CRLocation.VSeparateCheck">VSeparateCheck</see> option
  /// is turned on for the <see cref="T:PX.Objects.AP.Vendor">Vendor</see>.
  /// </summary>
  [PXString]
  public string HiddenKey
  {
    get => this._HiddenKey;
    set => this._HiddenKey = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch">Branch</see>, to which the document belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID">Branch.BranchID</see> field.
  /// </value>
  [Branch(null, null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  public virtual bool? Passed { get; set; }

  /// <summary>The type of the document.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <list>
  /// <item><description>INV: Invoice</description></item>
  /// <item><description>ACR: Credit Adjustment</description></item>
  /// <item><description>ADR: Debit Adjustment</description></item>
  /// <item><description>CHK: Payment</description></item>
  /// <item><description>VCK: Voided Payment</description></item>
  /// <item><description>PPM: Prepayment</description></item>
  /// <item><description>REF: Refund</description></item>
  /// <item><description>VRF: Voided Refund</description></item>
  /// <item><description>QCK: Cash Purchase</description></item>
  /// <item><description>VQC: Voided Cash Purchase</description></item>
  /// </list>
  /// </value>
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  [APDocType.List]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
  [PXFieldDescription]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXString]
  [PXUIField(DisplayName = "Document Type (Internal)")]
  public string InternalDocType
  {
    [PXDependsOnFields(new System.Type[] {typeof (APRegister.docType)})] get => this.DocType;
  }

  /// <summary>
  /// Type of the document for displaying in reports.
  /// This field has the same set of possible internal values as the <see cref="P:PX.Objects.AP.APRegister.DocType" /> field,
  /// but exposes different user-friendly values.
  /// </summary>
  /// <value>
  /// 
  /// </value>
  [PXString(3, IsFixed = true)]
  [APDocType.PrintList]
  [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.Visible, Enabled = true)]
  public virtual string PrintDocType
  {
    get => this._DocType;
    set
    {
    }
  }

  /// <summary>Reference number of the document.</summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
  [PXSelector(typeof (Search<APRegister.refNbr, Where<APRegister.docType, Equal<Optional<APRegister.docType>>>>), Filterable = true)]
  [PXFieldDescription]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  /// <summary>Module, from which the document originates.</summary>
  /// <value>
  /// Code of the module of the system. Defaults to "AP".
  /// Possible values are: "GL", "AP", "AR", "CM", "CA", "IN", "DR", "FA", "PM", "TX", "SO", "PO".
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXDefault("AP")]
  [PXUIField(DisplayName = "Source", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  [BatchModule.FullList]
  public virtual string OrigModule
  {
    get => this._OrigModule;
    set => this._OrigModule = value;
  }

  /// <summary>Date of the document.</summary>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  /// <summary>Date of the original (source) document.</summary>
  [PXDBDate]
  public virtual System.DateTime? OrigDocDate
  {
    get => this._OrigDocDate;
    set => this._OrigDocDate = value;
  }

  /// <summary>
  /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
  /// </summary>
  /// <value>
  /// Determined by the <see cref="P:PX.Objects.AP.APRegister.DocDate">date of the document</see>. Unlike <see cref="P:PX.Objects.AP.APRegister.FinPeriodID" />
  /// the value of this field can't be overriden by user.
  /// </value>
  [PeriodID(null, null, null, true)]
  [PXUIField(DisplayName = "Master Period")]
  public virtual string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  /// <summary>
  /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
  /// </summary>
  /// <value>
  /// Defaults to the period, to which the <see cref="P:PX.Objects.AP.APRegister.DocDate" /> belongs, but can be overriden by user.
  /// </value>
  [APOpenPeriod(typeof (APRegister.docDate), typeof (APRegister.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (APRegister.tranPeriodID), IsHeader = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.AP.Vendor" />, whom the document belongs to.
  /// </summary>
  [VendorActive(Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof (Vendor.acctName), CacheGlobal = true, Filterable = true)]
  [PXDefault]
  [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<APRegister.vendorID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  public virtual int? VendorID { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CR.Location">Location</see> of the <see cref="T:PX.Objects.AP.Vendor">Vendor</see>, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Location.LocationID" /> field. Defaults to vendor's <see cref="!:Vendor.DefLocationID">default location</see>.
  /// </value>
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Optional<APRegister.vendorID>>, PX.Data.And<MatchWithBranch<PX.Objects.CR.Location.vBranchID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(typeof (Coalesce<Search2<Vendor.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.locationID, Equal<Vendor.defLocationID>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Vendor.bAccountID>>>>, Where<Vendor.bAccountID, Equal<Current<APRegister.vendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, PX.Data.And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>, Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<APRegister.vendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, PX.Data.And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>>))]
  [PXForeignReference(typeof (CompositeKey<PX.Data.ReferentialIntegrity.Attributes.Field<APRegister.vendorID>.IsRelatedTo<PX.Objects.CR.Location.bAccountID>, PX.Data.ReferentialIntegrity.Attributes.Field<APRegister.vendorLocationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>>))]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  /// <summary>
  /// Code of the <see cref="T:PX.Objects.CM.Currency">Currency</see> of the document.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">company's base currency</see>.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <summary>
  /// Identifier of the AP account, to which the document belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault]
  [Account(typeof (APRegister.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, PX.Data.And<Where<Current<GLSetup.ytdNetIncAccountID>, PX.Data.IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AP Account", ControlAccountForModule = "AP")]
  public virtual int? APAccountID
  {
    get => this._APAccountID;
    set => this._APAccountID = value;
  }

  /// <summary>
  /// Identifier of the AP subaccount, to which the document belongs.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (APRegister.aPAccountID), typeof (APRegister.branchID), true, DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "AP Subaccount", Visibility = PXUIVisibility.Visible)]
  public virtual int? APSubID
  {
    get => this._APSubID;
    set => this._APSubID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Account">account</see> to which the Tax and Taxable amounts
  /// should be posted when the feature <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAP" /> is activated.
  /// The Cash account and Year-to-Date Net Income account cannot be selected as the value of this field.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
  /// </value>
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [Account(typeof (APRegister.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, PX.Data.And<Where<Current<GLSetup.ytdNetIncAccountID>, PX.Data.IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "Prepayment Account", ControlAccountForModule = "AP")]
  public virtual int? PrepaymentAccountID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Sub">subaccount</see> to which the Tax and Taxable amounts
  /// should be posted when the feature <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAP" /> is activated.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [SubAccount(typeof (APRegister.prepaymentAccountID), DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "Prepayment Subaccount", Visibility = PXUIVisibility.Visible)]
  public virtual int? PrepaymentSubID { get; set; }

  /// <summary>
  /// Counter of the document lines, used <i>internally</i> to assign numbers to newly created lines.
  /// It is not recommended to rely on this fields to determine the exact count of lines, because it might not reflect the latter under various conditions.
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
  /// <see cref="P:PX.Objects.AP.APAdjust.AdjNbr">numbers</see> to newly created <see cref="T:PX.Objects.AP.APAdjust">lines</see>.
  /// The value is used to determine old and new applications.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? AdjCntr { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.
  /// </summary>
  /// <value>
  /// Generated automatically. Corresponds to the <see cref="!:PX.Objects.CM.CurrencyInfo.CurrencyInfoID" /> field.
  /// </value>
  [PXDBLong]
  [PX.Objects.CM.Extensions.CurrencyInfo]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>
  /// The amount to be paid for the document in the currency of the document. (See <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.origDocAmt))]
  [PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Decimal? CuryOrigDocAmt
  {
    get => this._CuryOrigDocAmt;
    set => this._CuryOrigDocAmt = value;
  }

  /// <summary>
  /// The amount to be paid for the document in the base currency of the company. (See <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury(typeof (APRegister.branchID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? OrigDocAmt
  {
    get => this._OrigDocAmt;
    set => this._OrigDocAmt = value;
  }

  /// <summary>
  /// The balance of the Accounts Payable document after tax (if inclusive) and the discount in the currency of the document. (See <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [DynamicLabel(typeof (BqlOperand<labelUnpaidBalance, IBqlString>.When<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegister.docType, Equal<APDocType.prepaymentInvoice>>>>, PX.Data.And<BqlOperand<APRegister.status, IBqlString>.IsNotEqual<APDocStatus.unapplied>>>, PX.Data.And<BqlOperand<APRegister.status, IBqlString>.IsNotEqual<APDocStatus.closed>>>, PX.Data.And<BqlOperand<APRegister.status, IBqlString>.IsNotEqual<APDocStatus.voided>>>>.And<BqlOperand<APRegister.status, IBqlString>.IsNotEqual<APDocStatus.reserved>>>.Else<labelBalance>))]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.docBal), BaseCalc = false)]
  [PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual Decimal? CuryDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  /// <summary>
  /// The balance of the Accounts Payable document after tax (if inclusive) and the discount in the base currency of the company. (See <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury(typeof (APRegister.branchID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  /// <summary>
  /// The entered in migration mode balance of the document.
  /// Given in the <see cref="P:PX.Objects.AP.APRegister.CuryID">currency of the document</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.initDocBal))]
  [PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual Decimal? CuryInitDocBal { get; set; }

  /// <summary>
  /// The entered in migration mode balance of the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? InitDocBal { get; set; }

  [PXDBCalced(typeof (APRegister.curyInitDocBal), typeof (Decimal))]
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.initDocBal), BaseCalc = false)]
  [PXUIField(DisplayName = "Migrated Balance", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? DisplayCuryInitDocBal { get; set; }

  /// <summary>
  /// Total discount associated with the document in the base currency of the company. (See <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscTot
  {
    get => this._DiscTot;
    set => this._DiscTot = value;
  }

  /// <summary>
  /// Total discount associated with the document in the currency of the document. (See <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.discTot))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Discount Total", Enabled = true)]
  public virtual Decimal? CuryDiscTot
  {
    get => this._CuryDiscTot;
    set => this._CuryDiscTot = value;
  }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? DocDisc
  {
    get => this._DocDisc;
    set => this._DocDisc = value;
  }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.docDisc))]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Document Discount", Enabled = true)]
  public virtual Decimal? CuryDocDisc
  {
    get => this._CuryDocDisc;
    set => this._CuryDocDisc = value;
  }

  /// <summary>
  /// !REV! The amount of the cash discount taken for the original document.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.origDiscAmt))]
  [PXUIField(DisplayName = "Cash Discount", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual Decimal? CuryOrigDiscAmt
  {
    get => this._CuryOrigDiscAmt;
    set => this._CuryOrigDiscAmt = value;
  }

  /// <summary>
  /// The amount of the cash discount taken for the original document.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigDiscAmt
  {
    get => this._OrigDiscAmt;
    set => this._OrigDiscAmt = value;
  }

  /// <summary>
  /// !REV! The amount of the cash discount taken.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.discTaken))]
  public virtual Decimal? CuryDiscTaken
  {
    get => this._CuryDiscTaken;
    set => this._CuryDiscTaken = value;
  }

  /// <summary>
  /// The amount of the cash discount taken.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscTaken
  {
    get => this._DiscTaken;
    set => this._DiscTaken = value;
  }

  /// <summary>
  /// The difference between the cash discount that was available and the actual amount of cash discount taken.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.discBal), BaseCalc = false)]
  [PXUIField(DisplayName = "Cash Discount Balance", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual Decimal? CuryDiscBal
  {
    get => this._CuryDiscBal;
    set => this._CuryDiscBal = value;
  }

  /// <summary>
  /// The difference between the cash discount that was available and the actual amount of cash discount taken.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscBal
  {
    get => this._DiscBal;
    set => this._DiscBal = value;
  }

  /// <summary>
  /// The amount of withholding tax calculated for the document, if applicable, in the currency of the document. (See <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.origWhTaxAmt))]
  [PXUIField(DisplayName = "With. Tax", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public virtual Decimal? CuryOrigWhTaxAmt
  {
    get => this._CuryOrigWhTaxAmt;
    set => this._CuryOrigWhTaxAmt = value;
  }

  /// <summary>
  /// The amount of withholding tax calculated for the document, if applicable, in the base currency of the company. (See <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigWhTaxAmt
  {
    get => this._OrigWhTaxAmt;
    set => this._OrigWhTaxAmt = value;
  }

  /// <summary>
  /// !REV! The difference between the original amount of withholding tax to be payed and the amount that was actually paid.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.whTaxBal), BaseCalc = false)]
  public virtual Decimal? CuryWhTaxBal
  {
    get => this._CuryWhTaxBal;
    set => this._CuryWhTaxBal = value;
  }

  /// <summary>
  /// The difference between the original amount of withholding tax to be payed and the amount that was actually paid.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? WhTaxBal
  {
    get => this._WhTaxBal;
    set => this._WhTaxBal = value;
  }

  /// <summary>
  /// !REV! The amount of tax withheld from the payments to the document.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.taxWheld))]
  public virtual Decimal? CuryTaxWheld
  {
    get => this._CuryTaxWheld;
    set => this._CuryTaxWheld = value;
  }

  /// <summary>
  /// The amount of tax withheld from the payments to the document.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxWheld
  {
    get => this._TaxWheld;
    set => this._TaxWheld = value;
  }

  /// <summary>
  /// The amount of charges associated with the document in the currency of the document. (See <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.chargeAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Finance Charges", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual Decimal? CuryChargeAmt
  {
    get => this._CuryChargeAmt;
    set => this._CuryChargeAmt = value;
  }

  /// <summary>
  /// The amount of charges associated with the document in the base currency of the company. (See <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ChargeAmt
  {
    get => this._ChargeAmt;
    set => this._ChargeAmt = value;
  }

  /// <summary>Description of the document.</summary>
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string DocDesc
  {
    get => this._DocDesc;
    set => this._DocDesc = value;
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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual System.DateTime? LastModifiedDateTime
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
  /// Class of the document. This field is calculated based on the <see cref="P:PX.Objects.AP.APRegister.DocType" />.
  /// </summary>
  /// <value>
  /// Possible values are: "N" - for Invoice, Credit Adjustment, Debit Adjustment, Cash Purchase, Voided Cash Purchase and Cash Return ; "P" - for Payment, Voided Payment and Refund; "U" - for Prepayment.
  /// </value>
  [PXString(1, IsFixed = true)]
  public virtual string DocClass
  {
    [PXDependsOnFields(new System.Type[] {typeof (APRegister.docType)})] get
    {
      return APDocType.DocClass(this._DocType);
    }
    set
    {
    }
  }

  /// <summary>
  /// Number of the <see cref="T:PX.Objects.GL.Batch" />, generated for the document on release.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr">Batch.BatchNbr</see> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>))]
  [PX.Objects.GL.BatchNbr(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>), IsMigratedRecordField = typeof (APRegister.isMigratedRecord))]
  public virtual string BatchNbr { get; set; }

  /// <summary>
  /// Stores the number of the <see cref="T:PX.Objects.GL.Batch" /> generated during prebooking.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr">Batch.BatchNbr</see> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Pre-Releasing Batch Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PXSelector(typeof (Batch.batchNbr))]
  public virtual string PrebookBatchNbr
  {
    get => this._PrebookBatchNbr;
    set => this._PrebookBatchNbr = value;
  }

  /// <summary>
  /// Stores the number of the <see cref="T:PX.Objects.GL.Batch" /> generated when the document was voided.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr">Batch.BatchNbr</see> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Void Batch Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
  [PXSelector(typeof (Batch.batchNbr))]
  public virtual string VoidBatchNbr
  {
    get => this._VoidBatchNbr;
    set => this._VoidBatchNbr = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the document was released.
  /// </summary>
  [PX.Objects.GL.Released(PreventDeletingReleased = true)]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Visible = false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>
  /// When set, on persist checks, that the document has the corresponded <see cref="P:PX.Objects.AP.APRegister.Released" /> original value.
  /// When not set, on persist checks, that <see cref="P:PX.Objects.AP.APRegister.Released" /> value is not changed.
  /// Throws an error otherwise.
  /// </summary>
  [PXDBRestrictionBool(typeof (APRegister.released))]
  public virtual bool? ReleasedToVerify { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that the document is open.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Open", Visible = false)]
  public virtual bool? OpenDoc
  {
    get => this._OpenDoc;
    set => this._OpenDoc = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the document is on hold and thus cannot be released.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Hold", Visibility = PXUIVisibility.Visible)]
  [PXDefault(true, typeof (APSetup.holdEntry))]
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
  /// When set to <c>true</c> indicates that the document was voided. In this case <see cref="P:PX.Objects.AP.APRegister.VoidBatchNbr" /> field will hold the number of the voiding <see cref="T:PX.Objects.GL.Batch" />.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Void", Visible = false)]
  public virtual bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the document was printed.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Printed
  {
    get => this._Printed;
    set => this._Printed = value;
  }

  /// <summary>
  /// When set to <c>true</c> indicates that the document was prebooked.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Prebooked")]
  public virtual bool? Prebooked
  {
    get => this._Prebooked;
    set => this._Prebooked = value;
  }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? Approved { get; set; }

  [PXDBBool]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public bool? Rejected { get; set; }

  [PXDBBool]
  [PXDefault(true, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? DontApprove { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote(DescriptionField = typeof (APRegister.refNbr))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>!REV!</summary>
  [PXDBGuid(false)]
  public virtual Guid? RefNoteID
  {
    get => this._RefNoteID;
    set => this._RefNoteID = value;
  }

  /// <summary>The date of the last application.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Closed Date", Visibility = PXUIVisibility.Invisible)]
  public virtual System.DateTime? ClosedDate { get; set; }

  /// <summary>
  /// The <see cref="!:PX.Objects.GL.FinancialPeriod">Financial Period</see>, in which the document was closed.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AP.APRegister.FinPeriodID" /> field.
  /// </value>
  [PX.Objects.GL.FinPeriodID(null, typeof (APRegister.branchID), null, null, null, null, true, false, null, typeof (APRegister.closedTranPeriodID), null, true, true)]
  [PXUIField(DisplayName = "Closed Period", Visibility = PXUIVisibility.Invisible)]
  public virtual string ClosedFinPeriodID
  {
    get => this._ClosedFinPeriodID;
    set => this._ClosedFinPeriodID = value;
  }

  /// <summary>
  /// The <see cref="!:PX.Objects.GL.FinancialPeriod">Financial Period</see>, in which the document was closed.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AP.APRegister.TranPeriodID" /> field.
  /// </value>
  [PeriodID(null, null, null, true)]
  [PXUIField(DisplayName = "Closed Master Period", Visibility = PXUIVisibility.Invisible)]
  public virtual string ClosedTranPeriodID
  {
    get => this._ClosedTranPeriodID;
    set => this._ClosedTranPeriodID = value;
  }

  /// <summary>
  /// Realized Gain and Loss amount associated with the document.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RGOLAmt
  {
    get => this._RGOLAmt;
    set => this._RGOLAmt = value;
  }

  /// <summary>
  /// The difference between the original amount and the rounded amount in the currency of the document. (See <see cref="P:PX.Objects.AP.APRegister.CuryID" />)
  /// (Applicable only in case <see cref="P:PX.Objects.CS.FeaturesSet.InvoiceRounding">Invoice Rounding</see> feature is on.)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.roundDiff), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Rounding Diff.", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public Decimal? CuryRoundDiff { get; set; }

  /// <summary>
  /// The difference between the original amount and the rounded amount in the base currency of the company. (See <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// (Applicable only in case <see cref="P:PX.Objects.CS.FeaturesSet.InvoiceRounding">Invoice Rounding</see> feature is on.)
  /// </summary>
  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? RoundDiff { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.taxRoundDiff), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Rounding Diff.", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  public Decimal? CuryTaxRoundDiff { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? TaxRoundDiff { get; set; }

  /// <summary>
  /// Read-only field indicating whether the document is payable. Depends solely on the <see cref="P:PX.Objects.AP.APRegister.DocType">APRegister.DocType</see> field.
  /// Opposite to <see cref="P:PX.Objects.AP.APRegister.Paying" /> field.
  /// </summary>
  /// <value>
  /// <c>true</c> - for payable documents, e.g. bills; <c>false</c> - for paying, e.g. checks.
  /// </value>
  public virtual bool? Payable
  {
    [PXDependsOnFields(new System.Type[] {typeof (APRegister.docType)})] get
    {
      return APDocType.Payable(this._DocType);
    }
    set
    {
    }
  }

  /// <summary>
  /// Read-only field indicating whether the document is paying. Depends solely on the <see cref="P:PX.Objects.AP.APRegister.DocType">APRegister.DocType</see> field.
  /// Opposite to <see cref="P:PX.Objects.AP.APRegister.Payable" /> field.
  /// </summary>
  /// <value>
  /// <c>true</c> - for paying documents, e.g. checks; <c>false</c> - for payable ones, e.g. bills.
  /// </value>
  public virtual bool? Paying
  {
    [PXDependsOnFields(new System.Type[] {typeof (APRegister.docType)})] get
    {
      bool? nullable = APDocType.Payable(this._DocType);
      bool flag = false;
      return new bool?(nullable.GetValueOrDefault() == flag & nullable.HasValue);
    }
    set
    {
    }
  }

  /// <summary>
  /// Read-only field determining the sort order for AP documents based on the <see cref="P:PX.Objects.AP.APRegister.DocType" /> field.
  /// </summary>
  public virtual short? SortOrder
  {
    [PXDependsOnFields(new System.Type[] {typeof (APRegister.docType)})] get
    {
      return APDocType.SortOrder(this._DocType);
    }
    set
    {
    }
  }

  /// <summary>
  /// Read-only field indicating the sign of the document's impact on AP balance .
  /// Depends solely on the <see cref="P:PX.Objects.AP.APRegister.DocType" />
  /// </summary>
  /// <value>
  /// Can be <c>1</c>, <c>-1</c> or <c>0</c>.
  /// </value>
  public virtual Decimal? SignBalance
  {
    [PXDependsOnFields(new System.Type[] {typeof (APRegister.docType)})] get
    {
      return APDocType.SignBalance(this._DocType);
    }
    set
    {
    }
  }

  /// <summary>
  /// Read-only field indicating the sign of the document amount.
  /// Depends solely on the <see cref="P:PX.Objects.AP.APRegister.DocType" />
  /// </summary>
  /// <value>
  /// Can be <c>1</c>, <c>-1</c> or <c>0</c>.
  /// </value>
  public virtual Decimal? SignAmount
  {
    [PXDependsOnFields(new System.Type[] {typeof (APRegister.docType)})] get
    {
      return APDocType.SignAmount(this._DocType);
    }
    set
    {
    }
  }

  /// <summary>
  /// The status of the document. The field is calculated
  /// based on the values of the status flag. It can't be changed directly.
  /// The following fields determine the status of the document: <see cref="P:PX.Objects.AP.APRegister.Hold" />,
  /// <see cref="P:PX.Objects.AP.APRegister.Released" />, <see cref="P:PX.Objects.AP.APRegister.Voided" />, <see cref="P:PX.Objects.AP.APRegister.Scheduled" />,
  /// <see cref="P:PX.Objects.AP.APRegister.Prebooked" />, <see cref="P:PX.Objects.AP.APRegister.Printed" />, <see cref="P:PX.Objects.AP.APRegister.Approved" />, <see cref="P:PX.Objects.AP.APRegister.Rejected" />.
  /// </summary>
  /// <value>
  /// The field can have the following values:
  /// <c>"H"</c> - On Hold, <c>"B"</c> - Balanced, <c>"V"</c> - Voided, <c>"S"</c> - Scheduled,
  /// <c>"N"</c> - Open, <c>"C"</c> - Closed, <c>"P"</c> - Printed, <c>"K"</c> - Pre-Released,
  /// <c>"E"</c> - Pending Approval, <c>"R"</c> - Rejected, <c>"Z"</c> - Reserved.
  /// The value defaults to On Hold.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  [APDocStatus.List]
  [PXDependsOnFields(new System.Type[] {typeof (APRegister.voided), typeof (APRegister.hold), typeof (APRegister.scheduled), typeof (APRegister.released), typeof (APRegister.printed), typeof (APRegister.prebooked), typeof (APRegister.openDoc), typeof (APRegister.approved), typeof (APRegister.dontApprove), typeof (APRegister.rejected), typeof (APRegister.docType), typeof (APRegister.pendingProcessing)})]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Schedule">Schedule</see> object, associated with the document.
  /// In case <see cref="P:PX.Objects.AP.APRegister.Scheduled" /> is <c>true</c>, ScheduleID points to the Schedule, to which the document belongs as a template.
  /// Otherwise, ScheduleID points to the Schedule, from which this document was generated, if any.
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
  /// When <c>true</c>, indicates that the amount of tax calculated with the External Tax Provider is up to date.
  /// If this field equals <c>false</c>, the document was updated since last synchronization with the Tax Engine
  /// and taxes might need recalculation.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax Is Up to Date", Enabled = false)]
  public virtual bool? IsTaxValid { get; set; }

  /// <summary>
  /// When <c>true</c>, indicates that the tax information was successfully commited to the External Tax Provider.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax has been posted to the external tax provider", Enabled = false)]
  public virtual bool? IsTaxPosted { get; set; }

  /// <summary>
  /// Indicates whether the tax information related to the document was saved to the External Tax Provider.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax has been saved in the external tax provider", Enabled = false)]
  public virtual bool? IsTaxSaved { get; set; }

  /// <summary>
  /// Get or set NonTaxable that mark current document does not impose sales taxes.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Non-Taxable", Enabled = false)]
  public virtual bool? NonTaxable { get; set; }

  /// <summary>Type of the original (source) document.</summary>
  [PXDBString(3, IsFixed = true)]
  [APDocType.List]
  [PXUIField(DisplayName = "Orig. Doc. Type")]
  public virtual string OrigDocType
  {
    get => this._OrigDocType;
    set => this._OrigDocType = value;
  }

  /// <summary>Reference number of the original (source) document.</summary>
  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Orig. Ref. Nbr.")]
  public virtual string OrigRefNbr { get; set; }

  /// <summary>
  /// Read-only field that is equal to <c>true</c> in case the document
  /// was either <see cref="P:PX.Objects.AP.APRegister.Prebooked">prebooked</see> or
  /// <see cref="P:PX.Objects.AP.APRegister.Released">released</see>.
  /// </summary>
  [PXBool]
  public virtual bool? ReleasedOrPrebooked
  {
    [PXDependsOnFields(new System.Type[] {typeof (APRegister.released), typeof (APRegister.prebooked)})] get
    {
      return new bool?(this.Released.GetValueOrDefault() || this.Prebooked.GetValueOrDefault());
    }
    set
    {
    }
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (Search<PX.Objects.CR.Location.vTaxCalcMode, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APRegister.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APRegister.vendorLocationID>>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode
  {
    get => this._TaxCalcMode;
    set => this._TaxCalcMode = value;
  }

  internal string WarningMessage { get; set; }

  /// <summary>The workgroup that is responsible for the document.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.TM.EPCompanyTree.WorkGroupID">EPCompanyTree.WorkGroupID</see> field.
  /// </value>
  [PXDBInt]
  [PXDefault(typeof (APRegister.workgroupID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXCompanyTreeSelector]
  [PXUIField(DisplayName = "Workgroup ID", Enabled = false)]
  public virtual int? EmployeeWorkgroupID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.CR.Contact">Contact</see> responsible
  /// for the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [PXDefault(typeof (Coalesce<Search<CREmployee.defContactID, Where2<Where<CREmployee.userID, Equal<Current<AccessInfo.userID>>, Or<CREmployee.bAccountID, Equal<Current<APRegister.vendorID>>>>, And<CREmployee.vStatus, NotEqual<VendorStatus.inactive>, And<CREmployee.userID, PX.Data.IsNotNull>>>>, Search2<PX.Objects.CR.BAccount.ownerID, InnerJoin<CREmployee, On<CREmployee.defContactID, Equal<PX.Objects.CR.BAccount.ownerID>>>, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<APRegister.vendorID>>, And<CREmployee.vStatus, NotEqual<VendorStatus.inactive>>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  [Owner(typeof (APRegister.employeeWorkgroupID))]
  public virtual int? EmployeeID { get; set; }

  /// <summary>
  /// The workgroup that is responsible for document
  /// approval process.
  /// </summary>
  [PXInt]
  [PXSelector(typeof (Search<EPCompanyTree.workGroupID>), SubstituteKey = typeof (EPCompanyTree.description))]
  [PXUIField(DisplayName = "Approval Workgroup ID", Enabled = false)]
  public virtual int? WorkgroupID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.CR.Contact">contact</see> responsible
  /// for document approval process.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.Contact.ContactID" /> field.
  /// </value>
  [Owner(IsDBField = false, DisplayName = "Approver", Enabled = false)]
  public virtual int? OwnerID { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// in migration mode without affecting GL module.
  /// </summary>
  [MigratedRecord(typeof (APSetup.migrationMode))]
  public virtual bool? IsMigratedRecord { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// with activated <see cref="P:PX.Objects.CS.FeaturesSet.PaymentsByLines" /> feature and
  /// such document allow payments by lines.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Pay by Line", Visibility = PXUIVisibility.Visible, FieldClass = "PaymentsByLines")]
  [PXDefault(false)]
  public virtual bool? PaymentsByLinesAllowed { get; set; }

  [Account(typeof (APRegister.branchID), DisplayName = "Retainage Payable Account", DescriptionField = typeof (PX.Objects.GL.Account.description), ControlAccountForModule = "AP")]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual int? RetainageAcctID { get; set; }

  [SubAccount(typeof (APRegister.retainageAcctID), typeof (APRegister.branchID), true, DisplayName = "Retainage Payable Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual int? RetainageSubID { get; set; }

  [ProjectDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [APActiveProject]
  public virtual int? ProjectID { get; set; }

  /// <summary>
  /// A Boolean value that shows whether the AP document contains lines for different projects.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? HasMultipleProjects { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Apply Retainage", FieldClass = "Retainage")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? RetainageApply { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Retainage Document", Enabled = false, FieldClass = "Retainage")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? IsRetainageDocument { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Retainage Reversing", Enabled = false, FieldClass = "Retainage")]
  [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual bool? IsRetainageReversing { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Default Retainage Percent", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? DefRetainagePct { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.lineRetainageTotal))]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? CuryLineRetainageTotal { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? LineRetainageTotal { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.retainageTotal))]
  [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? CuryRetainageTotal { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
  public virtual Decimal? RetainageTotal { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.retainageUnreleasedAmt))]
  [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
  public virtual Decimal? RetainageUnreleasedAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.retainageReleased))]
  [PXUIField(DisplayName = "Released Retainage", FieldClass = "Retainage")]
  [PXFormula(typeof (Switch<Case<Where<APRegister.isRetainageReversing, Equal<True>>, decimal0>, Sub<APRegister.curyRetainageTotal, APRegister.curyRetainageUnreleasedAmt>>))]
  public virtual Decimal? CuryRetainageReleased { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Released Retainage", FieldClass = "Retainage")]
  public virtual Decimal? RetainageReleased { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.retainedTaxTotal))]
  [PXUIField(DisplayName = "Tax on Retainage", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? CuryRetainedTaxTotal { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? RetainedTaxTotal { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.retainedDiscTotal))]
  [PXUIField(DisplayName = "Discount on Retainage", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? CuryRetainedDiscTotal { get; set; }

  [PX.Objects.CM.Extensions.PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? RetainedDiscTotal { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.retainageUnpaidTotal))]
  [PXUIField(DisplayName = "Unpaid Retainage", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? CuryRetainageUnpaidTotal { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? RetainageUnpaidTotal { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.retainagePaidTotal))]
  [PXUIField(DisplayName = "Paid Retainage", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? CuryRetainagePaidTotal { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? RetainagePaidTotal { get; set; }

  [PX.Objects.CM.Extensions.PXCury(typeof (APRegister.curyID))]
  [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXFormula(typeof (Add<APRegister.curyOrigDocAmt, APRegister.curyRetainageTotal>))]
  public virtual Decimal? CuryOrigDocAmtWithRetainageTotal { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
  [PXFormula(typeof (Add<APRegister.origDocAmt, APRegister.retainageTotal>))]
  public virtual Decimal? OrigDocAmtWithRetainageTotal { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the prepayment is ready for payment applicaton
  /// when the feature <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAP" /> is activated.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PendingPayment { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.discountedDocTotal))]
  [PXUIField(DisplayName = "Discounted Doc. Total", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? CuryDiscountedDocTotal { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? DiscountedDocTotal
  {
    get => this._DiscountedDocTotal;
    set => this._DiscountedDocTotal = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.discountedTaxableTotal))]
  [PXUIField(DisplayName = "Discounted Taxable Total", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? CuryDiscountedTaxableTotal
  {
    get => this._CuryDiscountedTaxableTotal;
    set => this._CuryDiscountedTaxableTotal = value;
  }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? DiscountedTaxableTotal
  {
    get => this._DiscountedTaxableTotal;
    set => this._DiscountedTaxableTotal = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APRegister.curyInfoID), typeof (APRegister.discountedPrice))]
  [PXUIField(DisplayName = "Tax on Discounted Price", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? CuryDiscountedPrice
  {
    get => this._CuryDiscountedPrice;
    set => this._CuryDiscountedPrice = value;
  }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? DiscountedPrice
  {
    get => this._DiscountedPrice;
    set => this._DiscountedPrice = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? HasPPDTaxes
  {
    get => this._HasPPDTaxes;
    set => this._HasPPDTaxes = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PendingPPD
  {
    get => this._PendingPPD;
    set => this._PendingPPD = value;
  }

  /// <exclude />
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Adjustment Nbr.", Enabled = false, Visible = false, FieldClass = "DISTINV")]
  [PXSelector(typeof (Search<INRegister.refNbr, Where<INRegister.docType, Equal<INDocType.adjustment>>>))]
  public string TaxCostINAdjRefNbr { get; set; }

  /// <summary>
  /// Indicates that related <see cref="P:PX.Objects.AP.APTran.ExpectedPPVAmount" /> contains an actual values.
  /// If value is false, the <see cref="P:PX.Objects.AP.APTran.ExpectedPPVAmount" /> values will be recalculated during the document saving.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsExpectedPPVValid { get; set; }

  /// <summary>
  /// Set to true when external payment processor is used to process the payment and payment is not processed yet.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PendingProcessing { get; set; }

  public class PK : PrimaryKeyOf<APRegister>.By<APRegister.docType, APRegister.refNbr>
  {
    public static APRegister Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APRegister>.By<APRegister.docType, APRegister.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<APRegister>.By<APRegister.branchID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APRegister>.By<APRegister.vendorID>
    {
    }

    public class VendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<APRegister>.By<APRegister.vendorID, APRegister.vendorLocationID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APRegister>.By<APRegister.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<APRegister>.By<APRegister.curyID>
    {
    }

    public class APAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APRegister>.By<APRegister.aPAccountID>
    {
    }

    public class APSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<APRegister>.By<APRegister.aPSubID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<PX.Objects.GL.Schedule>.By<PX.Objects.GL.Schedule.scheduleID>.ForeignKeyOf<APRegister>.By<APRegister.scheduleID>
    {
    }

    public class RetainageAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APRegister>.By<APRegister.retainageAcctID>
    {
    }

    public class RetainageSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<APRegister>.By<APRegister.retainageSubID>
    {
    }

    public class OriginalDocument : 
      PrimaryKeyOf<APRegister>.By<APRegister.docType, APRegister.refNbr>.ForeignKeyOf<APRegister>.By<APRegister.origDocType, APRegister.origRefNbr>
    {
    }

    public class Employee : 
      PrimaryKeyOf<PX.Objects.EP.EPEmployee>.By<PX.Objects.EP.EPEmployee.bAccountID>.ForeignKeyOf<APRegister>.By<APRegister.employeeID>
    {
    }
  }

  public class Events : PXEntityEventBase<APRegister>.Container<APRegister.Events>
  {
    public PXEntityEvent<APRegister, PX.Objects.GL.Schedule> ConfirmSchedule;
    public PXEntityEvent<APRegister, PX.Objects.GL.Schedule> VoidSchedule;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.selected>
  {
  }

  public abstract class hiddenKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.hiddenKey>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegister.branchID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.docType>
  {
  }

  public abstract class printDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.printDocType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.refNbr>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.origModule>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APRegister.docDate>
  {
  }

  public abstract class origDocDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APRegister.origDocDate>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.tranPeriodID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.finPeriodID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegister.vendorID>
  {
  }

  public abstract class vendorID_Vendor_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegister.vendorID_Vendor_acctName>
  {
  }

  public abstract class vendorLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegister.vendorLocationID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.curyID>
  {
  }

  public abstract class aPAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegister.aPAccountID>
  {
  }

  public abstract class aPSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegister.aPSubID>
  {
  }

  public abstract class prepaymentAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRegister.prepaymentAccountID>
  {
  }

  public abstract class prepaymentSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegister.prepaymentSubID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegister.lineCntr>
  {
  }

  public abstract class adjCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegister.adjCntr>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APRegister.curyInfoID>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyOrigDocAmt>
  {
  }

  public abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.origDocAmt>
  {
  }

  public abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.curyDocBal>
  {
  }

  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.docBal>
  {
  }

  public abstract class curyInitDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyInitDocBal>
  {
  }

  public abstract class initDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.initDocBal>
  {
  }

  public abstract class displayCuryInitDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.displayCuryInitDocBal>
  {
  }

  public abstract class discTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.discTot>
  {
  }

  public abstract class curyDiscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.curyDiscTot>
  {
  }

  [Obsolete("This field is obsolete and will be removed in 2021R1.")]
  public abstract class docDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.docDisc>
  {
  }

  [Obsolete("This field is obsolete and will be removed in 2021R1.")]
  public abstract class curyDocDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.curyDocDisc>
  {
  }

  public abstract class curyOrigDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyOrigDiscAmt>
  {
  }

  public abstract class origDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.origDiscAmt>
  {
  }

  public abstract class curyDiscTaken : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.curyDiscTaken>
  {
  }

  public abstract class discTaken : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.discTaken>
  {
  }

  public abstract class curyDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.curyDiscBal>
  {
  }

  public abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.discBal>
  {
  }

  public abstract class curyOrigWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyOrigWhTaxAmt>
  {
  }

  public abstract class origWhTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.origWhTaxAmt>
  {
  }

  public abstract class curyWhTaxBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.curyWhTaxBal>
  {
  }

  public abstract class whTaxBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.whTaxBal>
  {
  }

  public abstract class curyTaxWheld : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.curyTaxWheld>
  {
  }

  public abstract class taxWheld : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.taxWheld>
  {
  }

  public abstract class curyChargeAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.curyChargeAmt>
  {
  }

  public abstract class chargeAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.chargeAmt>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.docDesc>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APRegister.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegister.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APRegister.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APRegister.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegister.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APRegister.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APRegister.Tstamp>
  {
  }

  public abstract class docClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.docClass>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.batchNbr>
  {
  }

  public abstract class prebookBatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegister.prebookBatchNbr>
  {
  }

  public abstract class voidBatchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.voidBatchNbr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.released>
  {
  }

  /// <exclude />
  public abstract class releasedToVerify : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.releasedToVerify>
  {
  }

  public abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.openDoc>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.hold>
  {
  }

  public abstract class scheduled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.scheduled>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.voided>
  {
  }

  public abstract class printed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.printed>
  {
  }

  public abstract class prebooked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.prebooked>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.rejected>
  {
  }

  public abstract class dontApprove : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.dontApprove>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APRegister.noteID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APRegister.refNoteID>
  {
  }

  public abstract class closedDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APRegister.closedDate>
  {
  }

  public abstract class closedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegister.closedFinPeriodID>
  {
  }

  public abstract class closedTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegister.closedTranPeriodID>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.rGOLAmt>
  {
  }

  public abstract class curyRoundDiff : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.curyRoundDiff>
  {
  }

  public abstract class roundDiff : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.roundDiff>
  {
  }

  public abstract class curyTaxRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyTaxRoundDiff>
  {
  }

  public abstract class taxRoundDiff : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APRegister.taxRoundDiff>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.status>
  {
  }

  public class SetStatusAttribute : 
    PXEventSubscriberAttribute,
    IPXRowUpdatingSubscriber,
    IPXRowInsertingSubscriber
  {
    public override void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      sender.Graph.FieldUpdating.AddHandler(sender.GetItemType(), "hold", (PXFieldUpdating) ((cache, e) =>
      {
        PXBoolAttribute.ConvertValue(e);
        if (!(e.Row is APRegister row2))
          return;
        this.StatusSet(cache, row2, (bool?) e.NewValue);
      }));
      sender.Graph.FieldVerifying.AddHandler(sender.GetItemType(), "status", (PXFieldVerifying) ((cache, e) => e.NewValue = cache.GetValue<APRegister.status>(e.Row)));
      sender.Graph.RowSelected.AddHandler(sender.GetItemType(), (PXRowSelected) ((cache, e) =>
      {
        if (!(e.Row is APRegister row4))
          return;
        this.StatusSet(cache, row4, row4.Hold);
      }));
    }

    protected virtual void StatusSet(PXCache cache, APRegister item, bool? HoldVal)
    {
      if (item.Voided.GetValueOrDefault())
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
        item.Status = "R";
      else if (!item.Released.GetValueOrDefault())
      {
        if (item.Printed.GetValueOrDefault() && item.DocType == "CHK")
          item.Status = "P";
        else if (item.Prebooked.GetValueOrDefault())
          item.Status = "K";
        else if (!item.Approved.GetValueOrDefault() && !item.DontApprove.GetValueOrDefault())
          item.Status = "E";
        else
          item.Status = "B";
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

    public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
    {
      APRegister row = (APRegister) e.Row;
      this.StatusSet(sender, row, row.Hold);
    }

    public virtual void RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
    {
      APRegister newRow = (APRegister) e.NewRow;
      this.StatusSet(sender, newRow, newRow.Hold);
    }
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.scheduleID>
  {
  }

  public abstract class impRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.impRefNbr>
  {
  }

  public abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.isTaxValid>
  {
  }

  public abstract class isTaxPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.isTaxPosted>
  {
  }

  public abstract class isTaxSaved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.isTaxSaved>
  {
  }

  public abstract class nonTaxable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.nonTaxable>
  {
  }

  public abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.origDocType>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.origRefNbr>
  {
  }

  public abstract class releasedOrPrebooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegister.releasedOrPrebooked>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APRegister.taxCalcMode>
  {
  }

  public abstract class employeeWorkgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APRegister.employeeWorkgroupID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegister.employeeID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegister.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegister.ownerID>
  {
  }

  public abstract class isMigratedRecord : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.isMigratedRecord>
  {
  }

  public abstract class paymentsByLinesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegister.paymentsByLinesAllowed>
  {
  }

  public abstract class retainageAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegister.retainageAcctID>
  {
  }

  public abstract class retainageSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegister.retainageSubID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APRegister.projectID>
  {
  }

  public abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegister.hasMultipleProjects>
  {
  }

  public abstract class retainageApply : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.retainageApply>
  {
  }

  public abstract class isRetainageDocument : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegister.isRetainageDocument>
  {
  }

  public abstract class isRetainageReversing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegister.isRetainageReversing>
  {
  }

  public abstract class defRetainagePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.defRetainagePct>
  {
  }

  public abstract class curyLineRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyLineRetainageTotal>
  {
  }

  public abstract class lineRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.lineRetainageTotal>
  {
  }

  public abstract class curyRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyRetainageTotal>
  {
  }

  public abstract class retainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.retainageTotal>
  {
  }

  public abstract class curyRetainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyRetainageUnreleasedAmt>
  {
  }

  public abstract class retainageUnreleasedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.retainageUnreleasedAmt>
  {
  }

  public abstract class curyRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyRetainageReleased>
  {
  }

  public abstract class retainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.retainageReleased>
  {
  }

  public abstract class curyRetainedTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyRetainedTaxTotal>
  {
  }

  public abstract class retainedTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.retainedTaxTotal>
  {
  }

  public abstract class curyRetainedDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyRetainedDiscTotal>
  {
  }

  public abstract class retainedDiscTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.retainedDiscTotal>
  {
  }

  public abstract class curyRetainageUnpaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyRetainageUnpaidTotal>
  {
  }

  public abstract class retainageUnpaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.retainageUnpaidTotal>
  {
  }

  public abstract class curyRetainagePaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyRetainagePaidTotal>
  {
  }

  public abstract class retainagePaidTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.retainagePaidTotal>
  {
  }

  public abstract class curyOrigDocAmtWithRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyOrigDocAmtWithRetainageTotal>
  {
  }

  public abstract class origDocAmtWithRetainageTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.origDocAmtWithRetainageTotal>
  {
  }

  public abstract class pendingPayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.pendingPayment>
  {
  }

  public abstract class curyDiscountedDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyDiscountedDocTotal>
  {
  }

  public abstract class discountedDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.discountedDocTotal>
  {
  }

  public abstract class curyDiscountedTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyDiscountedTaxableTotal>
  {
  }

  public abstract class discountedTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.discountedTaxableTotal>
  {
  }

  public abstract class curyDiscountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.curyDiscountedPrice>
  {
  }

  public abstract class discountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APRegister.discountedPrice>
  {
  }

  public abstract class hasPPDTaxes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.hasPPDTaxes>
  {
  }

  public abstract class pendingPPD : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APRegister.pendingPPD>
  {
  }

  /// <exclude />
  public abstract class taxCostINAdjRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APRegister.taxCostINAdjRefNbr>
  {
  }

  public abstract class isExpectedPPVValid : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegister.isExpectedPPVValid>
  {
  }

  public abstract class pendingProcessing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APRegister.pendingProcessing>
  {
  }
}
