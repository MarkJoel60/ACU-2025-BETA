// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOAdjust
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.SO.DAC.Projections;
using PX.Objects.SO.Interfaces;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("Sales Order Adjust")]
public class SOAdjust : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAdjustment,
  ICreatePaymentAdjust
{
  protected int? _RecordID;
  protected bool? _Hold;
  protected int? _CustomerID;
  protected 
  #nullable disable
  string _AdjgDocType;
  public const int AdjgDocTypeLength = 3;
  protected string _AdjgRefNbr;
  public const int AdjgRefNbrLength = 15;
  protected string _AdjdOrderType;
  protected string _AdjdOrderNbr;
  protected Decimal? _CuryAdjgAmt;
  protected Decimal? _AdjAmt;
  protected Decimal? _CuryAdjdAmt;
  protected Decimal? _CuryOrigAdjdAmt;
  protected Decimal? _OrigAdjAmt;
  protected Decimal? _CuryOrigAdjgAmt;
  protected long? _AdjdOrigCuryInfoID;
  protected long? _AdjgCuryInfoID;
  protected long? _AdjdCuryInfoID;
  protected DateTime? _AdjgDocDate;
  protected DateTime? _AdjdOrderDate;
  protected Decimal? _CuryAdjgBilledAmt;
  protected Decimal? _AdjBilledAmt;
  protected Decimal? _CuryAdjdBilledAmt;
  protected Decimal? _CuryDocBal;
  protected Decimal? _DocBal;
  protected bool? _Voided;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected string _PaymentMethodID;
  protected int? _CashAccountID;
  protected string _ExtRefNbr;
  protected Decimal? _OrigDocAmt;

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXDBBool]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXDBInt]
  [PXDefault(typeof (PX.Objects.AR.ARPayment.customerID))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [ARPaymentType.List]
  [PXDefault(typeof (PX.Objects.AR.ARPayment.docType))]
  [PXUIField(DisplayName = "Doc. Type", Enabled = false, Visible = false)]
  public virtual string AdjgDocType
  {
    get => this._AdjgDocType;
    set => this._AdjgDocType = value;
  }

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDBDefault(typeof (PX.Objects.AR.ARRegister.refNbr), DefaultForUpdate = false)]
  [PXUIField(DisplayName = "Reference Nbr.", Enabled = false, Visible = false)]
  [PXParent(typeof (Select<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARPayment.docType, Equal<Current<SOAdjust.adjgDocType>>, And<PX.Objects.AR.ARPayment.refNbr, Equal<Current<SOAdjust.adjgRefNbr>>>>>))]
  [PXParent(typeof (Select<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Current<SOAdjust.adjgDocType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Current<SOAdjust.adjgRefNbr>>>>>))]
  [PXParent(typeof (Select<ARPaymentTotals, Where<ARPaymentTotals.docType, Equal<Current<SOAdjust.adjgDocType>>, And<ARPaymentTotals.refNbr, Equal<Current<SOAdjust.adjgRefNbr>>>>>), ParentCreate = true)]
  public virtual string AdjgRefNbr
  {
    get => this._AdjgRefNbr;
    set => this._AdjgRefNbr = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true, InputMask = ">aa")]
  [PXDefault(typeof (Switch<Case<Where<Current<SOAdjust.adjgDocType>, Equal<ARDocType.refund>>, SOOrderTypeConstants.creditMemo>, SOOrderTypeConstants.salesOrder>))]
  [PXUIField(DisplayName = "Order Type")]
  [PXSelector(typeof (Search<SOOrderType.orderType, Where<SOOrderType.active, Equal<True>, And<Where<Current<SOAdjust.adjgDocType>, NotEqual<ARDocType.refund>, And<SOOrderType.canHavePayments, Equal<True>, Or<Current<SOAdjust.adjgDocType>, Equal<ARDocType.refund>, And<SOOrderType.canHaveRefunds, Equal<True>>>>>>>>))]
  public virtual string AdjdOrderType
  {
    get => this._AdjdOrderType;
    set => this._AdjdOrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Order Nbr.")]
  [PXParent(typeof (SOAdjust.FK.Order))]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOAdjust.curyAdjdAmt, Greater<decimal0>>, int1>, int0>), typeof (SumCalc<SOOrder.paymentCntr>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOAdjust.curyAdjdAmt, Greater<decimal0>, And<SOAdjust.isCCAuthorized, Equal<True>, And<SOAdjust.isCCCaptured, Equal<False>>>>, int1>, int0>), typeof (SumCalc<SOOrder.authorizedPaymentCntr>))]
  [PXRestrictor(typeof (Where<SOOrder.status, NotIn3<SOOrderStatus.cancelled, SOOrderStatus.pendingApproval, SOOrderStatus.voided>>), "No payment can be created for a sales order with any of the following statuses: Voided, Cancelled, or Pending approval.", new Type[] {})]
  [PXSelector(typeof (Search2<SOOrder.orderNbr, InnerJoin<SOOrderType, On<SOOrderType.orderType, Equal<SOOrder.orderType>>, LeftJoin<PX.Objects.CS.Terms, On<PX.Objects.CS.Terms.termsID, Equal<SOOrder.termsID>>>>, Where2<Where<SOOrder.customerID, Equal<Current<SOAdjust.customerID>>, Or<SOOrder.customerID, In2<Search<PX.Objects.AR.Customer.bAccountID, Where<PX.Objects.AR.Customer.consolidatingBAccountID, Equal<Current<SOAdjust.customerID>>>>>>>, And<SOOrder.orderType, Equal<Optional<SOAdjust.adjdOrderType>>, And<SOOrder.openDoc, Equal<boolTrue>, And2<Where<SOOrder.behavior, NotEqual<SOBehavior.bL>, Or<Where<SOOrder.hold, NotEqual<True>, And<SOOrder.isExpired, NotEqual<True>, And<SOOrder.childLineCntr, Equal<int0>>>>>>, And<Where<PX.Objects.CS.Terms.termsID, IsNull, Or<PX.Objects.CS.Terms.installmentType, NotEqual<TermsInstallmentType.multiple>>>>>>>>>), new Type[] {typeof (SOOrder.orderNbr), typeof (SOOrder.orderDate), typeof (SOOrder.finPeriodID), typeof (SOOrder.customerLocationID), typeof (SOOrder.curyID), typeof (SOOrder.curyOrderTotal), typeof (SOOrder.curyOpenOrderTotal), typeof (SOOrder.status), typeof (SOOrder.dueDate), typeof (SOOrder.invoiceNbr), typeof (SOOrder.orderDesc)}, Filterable = true)]
  public virtual string AdjdOrderNbr
  {
    get => this._AdjdOrderNbr;
    set => this._AdjdOrderNbr = value;
  }

  [PXDBCurrency(typeof (SOAdjust.adjgCuryInfoID), typeof (SOAdjust.adjAmt))]
  [PXUIField(DisplayName = "Applied To Order")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjgAmt
  {
    get => this._CuryAdjgAmt;
    set => this._CuryAdjgAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjAmt
  {
    get => this._AdjAmt;
    set => this._AdjAmt = value;
  }

  [PXDBDecimal(4)]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOAdjust.voided, Equal<False>, And<SOAdjust.isCCAuthorized, Equal<False>, And<Where<SOAdjust.paymentReleased, Equal<False>, Or<SOAdjust.pendingPayment, Equal<True>>>>>>, SOAdjust.curyAdjdAmt>, decimal0>), typeof (SumCalc<SOOrder.curyUnreleasedPaymentAmt>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOAdjust.voided, Equal<False>, And<SOAdjust.paymentReleased, Equal<False>, And<SOAdjust.isCCAuthorized, Equal<True>>>>, SOAdjust.curyAdjdAmt>, decimal0>), typeof (SumCalc<SOOrder.curyCCAuthorizedAmt>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOAdjust.voided, Equal<False>, And<SOAdjust.paymentReleased, Equal<True>, And<SOAdjust.pendingPayment, Equal<False>>>>, SOAdjust.curyAdjdAmt>, decimal0>), typeof (SumCalc<SOOrder.curyPaidAmt>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOAdjust.voided, Equal<False>>, SOAdjust.curyAdjdAmt>, decimal0>), typeof (SumCalc<SOOrder.curyPaymentTotal>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (IIf<Where<SOAdjust.voided, Equal<False>, And2<Where<SOAdjust.adjgDocType, NotEqual<ARDocType.prepaymentInvoice>, Or<SOAdjust.paymentReleased, Equal<True>>>, And<Where<SOAdjust.isCCPayment, Equal<False>, Or<SOAdjust.isCCAuthorized, Equal<True>, Or<SOAdjust.isCCCaptured, Equal<True>, Or<SOAdjust.paymentReleased, Equal<True>>>>>>>>, Add<SOAdjust.curyAdjdAmt, SOAdjust.curyAdjdBilledAmt>, decimal0>), typeof (SumCalc<SOOrder.curyPaymentOverall>), ForceAggregateRecalculation = true)]
  [CopyChildLink(typeof (ARPaymentTotals.orderCntr), typeof (SOAdjust.curyAdjdAmt), new Type[] {typeof (SOAdjust.adjdOrderType), typeof (SOAdjust.adjdOrderNbr)}, new Type[] {typeof (ARPaymentTotals.adjdOrderType), typeof (ARPaymentTotals.adjdOrderNbr)})]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjdAmt
  {
    get => this._CuryAdjdAmt;
    set => this._CuryAdjdAmt = value;
  }

  [PXDBCalced(typeof (Add<SOAdjust.curyAdjdAmt, SOAdjust.curyAdjdBilledAmt>), typeof (Decimal), Persistent = true)]
  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrigAdjdAmt
  {
    get => this._CuryOrigAdjdAmt;
    set => this._CuryOrigAdjdAmt = value;
  }

  [PXDBCalced(typeof (Add<SOAdjust.adjAmt, SOAdjust.adjBilledAmt>), typeof (Decimal), Persistent = true)]
  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigAdjAmt
  {
    get => this._OrigAdjAmt;
    set => this._OrigAdjAmt = value;
  }

  [PXDBCalced(typeof (Add<SOAdjust.curyAdjgAmt, SOAdjust.curyAdjgBilledAmt>), typeof (Decimal), Persistent = true)]
  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrigAdjgAmt
  {
    get => this._CuryOrigAdjgAmt;
    set => this._CuryOrigAdjgAmt = value;
  }

  [PXDBCalced(typeof (Add<SOAdjust.curyAdjdAmt, SOAdjust.curyAdjdTransferredToChildrenAmt>), typeof (Decimal), Persistent = true)]
  [PXDecimal(4)]
  public virtual Decimal? CuryAdjdOrigBlanketAmt { get; set; }

  [PXDBCalced(typeof (Add<SOAdjust.adjAmt, SOAdjust.adjTransferredToChildrenAmt>), typeof (Decimal), Persistent = true)]
  [PXDecimal(4)]
  public virtual Decimal? AdjOrigBlanketAmt { get; set; }

  [PXDBCalced(typeof (Add<SOAdjust.curyAdjgAmt, SOAdjust.curyAdjgTransferredToChildrenAmt>), typeof (Decimal), Persistent = true)]
  [PXDecimal(4)]
  public virtual Decimal? CuryAdjgOrigBlanketAmt { get; set; }

  [PXDecimal(4)]
  public Decimal? CuryAdjgDiscAmt
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  [PXDecimal(4)]
  public Decimal? CuryAdjdDiscAmt
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  [PXDecimal(4)]
  public Decimal? AdjDiscAmt
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  [PXDBLong]
  [PXDefault]
  [CurrencyInfo(typeof (PX.Objects.AR.ARInvoice.curyInfoID))]
  public virtual long? AdjdOrigCuryInfoID
  {
    get => this._AdjdOrigCuryInfoID;
    set => this._AdjdOrigCuryInfoID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (PX.Objects.AR.ARPayment.curyInfoID))]
  public virtual long? AdjgCuryInfoID
  {
    get => this._AdjgCuryInfoID;
    set => this._AdjgCuryInfoID = value;
  }

  [PXDBLong]
  [PXDefault]
  [CurrencyInfo(typeof (PX.Objects.AR.ARPayment.curyInfoID))]
  public virtual long? AdjdCuryInfoID
  {
    get => this._AdjdCuryInfoID;
    set => this._AdjdCuryInfoID = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? AdjgDocDate
  {
    get => this._AdjgDocDate;
    set => this._AdjgDocDate = value;
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? AdjdOrderDate
  {
    get => this._AdjdOrderDate;
    set => this._AdjdOrderDate = value;
  }

  [PXDBCurrency(typeof (SOAdjust.adjgCuryInfoID), typeof (SOAdjust.adjBilledAmt), BaseCalc = false)]
  [PXUIField(DisplayName = "Transferred to Invoice", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjgBilledAmt
  {
    get => this._CuryAdjgBilledAmt;
    set => this._CuryAdjgBilledAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjBilledAmt
  {
    get => this._AdjBilledAmt;
    set => this._AdjBilledAmt = value;
  }

  [PXDBCurrency(typeof (SOAdjust.adjdCuryInfoID), typeof (SOAdjust.adjBilledAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transferred to Invoice", Enabled = false)]
  [PXUnboundFormula(typeof (IIf<Where<SOAdjust.voided, Equal<False>>, SOAdjust.curyAdjdBilledAmt, decimal0>), typeof (SumCalc<SOOrder.curyBilledPaymentTotal>), ForceAggregateRecalculation = true)]
  public virtual Decimal? CuryAdjdBilledAmt
  {
    get => this._CuryAdjdBilledAmt;
    set => this._CuryAdjdBilledAmt = value;
  }

  [PXDBCurrency(typeof (SOAdjust.adjgCuryInfoID), typeof (SOAdjust.adjTransferredToChildrenAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transferred to Child Orders", Enabled = false)]
  public virtual Decimal? CuryAdjgTransferredToChildrenAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjTransferredToChildrenAmt { get; set; }

  [PXDBCurrency(typeof (SOAdjust.adjdCuryInfoID), typeof (SOAdjust.adjTransferredToChildrenAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transferred to Child Orders", Enabled = false, Visible = false)]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOAdjust.voided, Equal<False>>, Add<SOAdjust.curyAdjgAmt, SOAdjust.curyAdjgBilledAmt>>, decimal0>), typeof (SumCalc<BlanketSOAdjust.curyAdjgTransferredToChildrenAmt>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOAdjust.voided, Equal<False>>, Add<SOAdjust.adjAmt, SOAdjust.adjBilledAmt>>, decimal0>), typeof (SumCalc<BlanketSOAdjust.adjTransferredToChildrenAmt>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOAdjust.voided, Equal<False>>, Add<SOAdjust.curyAdjdAmt, SOAdjust.curyAdjdBilledAmt>>, decimal0>), typeof (SumCalc<BlanketSOAdjust.curyAdjdTransferredToChildrenAmt>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (IIf<Where<SOAdjust.voided, Equal<False>>, SOAdjust.curyAdjdTransferredToChildrenAmt, decimal0>), typeof (SumCalc<SOOrder.curyTransferredToChildrenPaymentTotal>), ForceAggregateRecalculation = true)]
  public virtual Decimal? CuryAdjdTransferredToChildrenAmt { get; set; }

  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (SOAdjust.adjgCuryInfoID), typeof (SOAdjust.docBal), BaseCalc = false)]
  [PXUIField]
  public virtual Decimal? CuryDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  [PXDecimal(4)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  [PXDBBool]
  [DenormalizedFrom(new Type[] {typeof (PX.Objects.AR.ARPayment.isCCPayment), typeof (PX.Objects.AR.ARPayment.released), typeof (PX.Objects.AR.ARPayment.isCCAuthorized), typeof (PX.Objects.AR.ARPayment.isCCCaptured), typeof (PX.Objects.AR.ARPayment.voided), typeof (PX.Objects.AR.ARPayment.hold), typeof (PX.Objects.AR.ARPayment.adjDate), typeof (PX.Objects.AR.ARPayment.paymentMethodID), typeof (PX.Objects.AR.ARPayment.cashAccountID), typeof (PX.Objects.AR.ARPayment.pMInstanceID), typeof (PX.Objects.AR.ARPayment.processingCenterID), typeof (PX.Objects.AR.ARPayment.extRefNbr), typeof (PX.Objects.AR.ARRegister.docDesc), typeof (PX.Objects.AR.ARPayment.curyOrigDocAmt), typeof (PX.Objects.AR.ARPayment.origDocAmt), typeof (PX.Objects.AR.ARPayment.syncLock), typeof (PX.Objects.AR.ARPayment.syncLockReason), typeof (PX.Objects.AR.ARPayment.pendingPayment)}, new Type[] {typeof (SOAdjust.isCCPayment), typeof (SOAdjust.paymentReleased), typeof (SOAdjust.isCCAuthorized), typeof (SOAdjust.isCCCaptured), typeof (SOAdjust.voided), typeof (SOAdjust.hold), typeof (SOAdjust.adjgDocDate), typeof (SOAdjust.paymentMethodID), typeof (SOAdjust.cashAccountID), typeof (SOAdjust.pMInstanceID), typeof (SOAdjust.processingCenterID), typeof (SOAdjust.extRefNbr), typeof (SOAdjust.docDesc), typeof (SOAdjust.curyOrigDocAmt), typeof (SOAdjust.origDocAmt), typeof (SOAdjust.syncLock), typeof (SOAdjust.syncLockReason), typeof (SOAdjust.pendingPayment)}, null, null)]
  public virtual bool? IsCCPayment { get; set; }

  [PXDBBool]
  public virtual bool? PaymentReleased { get; set; }

  [PXDBBool]
  public virtual bool? IsCCAuthorized { get; set; }

  [PXDBBool]
  public virtual bool? IsCCCaptured { get; set; }

  [PXDBBool]
  public virtual bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  [PXDBInt]
  [PXParent(typeof (SOAdjust.FK.BlanketAdjustment))]
  public virtual int? BlanketRecordID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Blanket SO Type", Enabled = false)]
  public virtual string BlanketType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXParent(typeof (SOAdjust.FK.BlanketOrder))]
  [PXUnboundFormula(typeof (BqlOperand<int1, IBqlInt>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOAdjust.blanketNbr, IsNotNull>>>>.And<BqlOperand<SOAdjust.blanketNbr, IBqlString>.IsNotEqual<Empty>>>.Else<int0>), typeof (SumCalc<SOOrder.blanketSOAdjustCntr>))]
  [PXUIField(DisplayName = "Blanket SO Ref. Nbr.", Enabled = false)]
  public virtual string BlanketNbr { get; set; }

  [PXString(50, IsUnicode = true)]
  public virtual string RefTranExtNbr { get; set; }

  [PXString(80 /*0x50*/, IsUnicode = true)]
  public virtual string ExternalRef { get; set; }

  [PXBool]
  public virtual bool? Authorize { get; set; }

  [PXBool]
  public virtual bool? Capture { get; set; }

  [PXBool]
  public virtual bool? Refund { get; set; }

  [PXDBBool]
  [PXDefault(typeof (Search<SOOrderType.validateCCRefundsOrigTransactions, Where<SOOrderType.orderType, Equal<Current<SOAdjust.adjdOrderType>>>>))]
  public virtual bool? ValidateCCRefundOrigTransaction { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>
  /// Indicates that balance for application must be recalculated instead of be adjusted for applied amount delta.
  /// </summary>
  [PXBool]
  public bool? IsBalanceRecalculationRequired { get; set; }

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
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public Decimal? CuryPayDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  public Decimal? PayDocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  public Decimal? CuryPayDiscBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? PayDiscBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? CuryPayWhTaxBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? PayWhTaxBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public DateTime? AdjdDocDate
  {
    get => this._AdjdOrderDate;
    set => this._AdjdOrderDate = value;
  }

  public Decimal? RGOLAmt
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public bool? Released
  {
    get => new bool?(false);
    set
    {
    }
  }

  public bool? ReverseGainLoss
  {
    get => new bool?(false);
    set
    {
    }
  }

  public Decimal? CuryDiscBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? DiscBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? CuryAdjgWhTaxAmt
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? CuryAdjdWhTaxAmt
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? AdjWhTaxAmt
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? CuryWhTaxBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public Decimal? WhTaxBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Payment Method")]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  public virtual string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  [PXUIField]
  [CashAccount(typeof (PX.Objects.AR.ARPayment.branchID), typeof (Search<PX.Objects.CA.CashAccount.cashAccountID>))]
  public virtual int? CashAccountID
  {
    get => this._CashAccountID;
    set => this._CashAccountID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>), DescriptionField = typeof (PX.Objects.AR.CustomerPaymentMethod.descr))]
  public virtual int? PMInstanceID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Proc. Center ID")]
  [PXSelector(typeof (Search<CCProcessingCenter.processingCenterID>), DescriptionField = typeof (CCProcessingCenter.name), ValidateValue = false)]
  public virtual string ProcessingCenterID { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField]
  public virtual string ExtRefNbr { get; set; }

  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXDefault(typeof (PX.Objects.AR.ARRegister.docDesc))]
  [PXUIField]
  public virtual string DocDesc { get; set; }

  [PXDBCurrency(typeof (SOAdjust.adjgCuryInfoID), typeof (SOAdjust.origDocAmt))]
  [PXUIField]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  [PXDBBaseCury]
  public virtual Decimal? OrigDocAmt
  {
    get => this._OrigDocAmt;
    set => this._OrigDocAmt = value;
  }

  [PXUnboundFormula(typeof (IIf<Where<SOAdjust.isCCPayment, Equal<True>, And<SOAdjust.syncLock, Equal<True>, And<SOAdjust.syncLockReason, NotEqual<PX.Objects.AR.ARPayment.syncLockReason.newCard>, And<SOAdjust.curyAdjdAmt, NotEqual<decimal0>>>>>, int1, Zero>), typeof (SumCalc<SOOrder.paymentsNeedValidationCntr>), ForceAggregateRecalculation = true)]
  [PXDBBool]
  public virtual bool? SyncLock { get; set; }

  [PXDBString(1, IsFixed = true, IsUnicode = false)]
  public virtual string SyncLockReason { get; set; }

  [PXBool]
  public virtual bool? NewCard { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Save Card")]
  public virtual bool? SaveCard { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the prepayment ready for payment applicaton
  /// when the feature <see cref="P:PX.Objects.CS.FeaturesSet.VATRecognitionOnPrepaymentsAR" /> is activated.
  /// </summary>
  [PXDBBool]
  public virtual bool? PendingPayment { get; set; }

  public class PK : 
    PrimaryKeyOf<SOAdjust>.By<SOAdjust.recordID, SOAdjust.adjdOrderType, SOAdjust.adjdOrderNbr, SOAdjust.adjgDocType, SOAdjust.adjgRefNbr>
  {
    public static SOAdjust Find(
      PXGraph graph,
      int recordID,
      string adjdOrderType,
      string adjdOrderNbr,
      string adjgDocType,
      string adjgRefNbr,
      PKFindOptions options = 0)
    {
      return (SOAdjust) PrimaryKeyOf<SOAdjust>.By<SOAdjust.recordID, SOAdjust.adjdOrderType, SOAdjust.adjdOrderNbr, SOAdjust.adjgDocType, SOAdjust.adjgRefNbr>.FindBy(graph, (object) recordID, (object) adjdOrderType, (object) adjdOrderNbr, (object) adjgDocType, (object) adjgRefNbr, options);
    }
  }

  public static class FK
  {
    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOAdjust>.By<SOAdjust.adjdOrderType, SOAdjust.adjdOrderNbr>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<SOAdjust>.By<SOAdjust.customerID>
    {
    }

    public class AdjustingPayment : 
      PrimaryKeyOf<PX.Objects.AR.ARPayment>.By<PX.Objects.AR.ARPayment.docType, PX.Objects.AR.ARPayment.refNbr>.ForeignKeyOf<SOAdjust>.By<SOAdjust.adjgDocType, SOAdjust.adjgRefNbr>
    {
    }

    public class CurrencyInfoOfAdjustingPayment : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<SOAdjust>.By<SOAdjust.adjgCuryInfoID>
    {
    }

    public class AdjustedOrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOAdjust>.By<SOAdjust.adjdOrderType>
    {
    }

    public class AdjustedOrder : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOAdjust>.By<SOAdjust.adjgDocType, SOAdjust.adjgRefNbr>
    {
    }

    public class CurrencyInfoOfAdjustedOrder : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<SOAdjust>.By<SOAdjust.adjdCuryInfoID>
    {
    }

    public class OriginalCurrencyInfoOfAdjustedOrder : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<SOAdjust>.By<SOAdjust.adjdOrigCuryInfoID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<SOAdjust>.By<SOAdjust.paymentMethodID>
    {
    }

    public class CustomerPaymentMethod : 
      PrimaryKeyOf<PX.Objects.AR.CustomerPaymentMethod>.By<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>.ForeignKeyOf<SOAdjust>.By<SOAdjust.pMInstanceID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<SOAdjust>.By<SOAdjust.cashAccountID>
    {
    }

    public class ProcessingCenter : 
      PrimaryKeyOf<CCProcessingCenter>.By<CCProcessingCenter.processingCenterID>.ForeignKeyOf<SOAdjust>.By<SOAdjust.processingCenterID>
    {
    }

    public class BlanketOrder : 
      PrimaryKeyOf<BlanketSOOrder>.By<BlanketSOOrder.orderType, BlanketSOOrder.orderNbr>.ForeignKeyOf<SOAdjust>.By<SOAdjust.blanketType, SOAdjust.blanketNbr>
    {
    }

    public class BlanketAdjustment : 
      PrimaryKeyOf<BlanketSOAdjust>.By<BlanketSOAdjust.recordID, BlanketSOAdjust.adjdOrderType, BlanketSOAdjust.adjdOrderNbr, BlanketSOAdjust.adjgDocType, BlanketSOAdjust.adjgRefNbr>.ForeignKeyOf<SOAdjust>.By<SOAdjust.blanketRecordID, SOAdjust.blanketType, SOAdjust.blanketNbr, SOAdjust.adjgDocType, SOAdjust.adjgRefNbr>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOAdjust.recordID>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAdjust.hold>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOAdjust.customerID>
  {
  }

  public abstract class adjgDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAdjust.adjgDocType>
  {
  }

  public abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAdjust.adjgRefNbr>
  {
  }

  public abstract class adjdOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAdjust.adjdOrderType>
  {
  }

  public abstract class adjdOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAdjust.adjdOrderNbr>
  {
  }

  public abstract class curyAdjgAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOAdjust.curyAdjgAmt>
  {
  }

  public abstract class adjAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOAdjust.adjAmt>
  {
  }

  public abstract class curyAdjdAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOAdjust.curyAdjdAmt>
  {
  }

  public abstract class curyOrigAdjdAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOAdjust.curyOrigAdjdAmt>
  {
  }

  public abstract class origAdjAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOAdjust.origAdjAmt>
  {
  }

  public abstract class curyOrigAdjgAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOAdjust.curyOrigAdjgAmt>
  {
  }

  public abstract class curyAdjdOrigBlanketAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOAdjust.curyAdjdOrigBlanketAmt>
  {
  }

  public abstract class adjOrigBlanketAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOAdjust.adjOrigBlanketAmt>
  {
  }

  public abstract class curyAdjgOrigBlanketAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOAdjust.curyAdjgOrigBlanketAmt>
  {
  }

  public abstract class curyAdjgDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOAdjust.curyAdjgDiscAmt>
  {
  }

  public abstract class curyAdjdDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOAdjust.curyAdjdDiscAmt>
  {
  }

  public abstract class adjDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOAdjust.adjDiscAmt>
  {
  }

  public abstract class adjdOrigCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    SOAdjust.adjdOrigCuryInfoID>
  {
  }

  public abstract class adjgCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOAdjust.adjgCuryInfoID>
  {
  }

  public abstract class adjdCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOAdjust.adjdCuryInfoID>
  {
  }

  public abstract class adjgDocDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOAdjust.adjgDocDate>
  {
  }

  public abstract class adjdOrderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOAdjust.adjdOrderDate>
  {
  }

  public abstract class curyAdjgBilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOAdjust.curyAdjgBilledAmt>
  {
  }

  public abstract class adjBilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOAdjust.adjBilledAmt>
  {
  }

  public abstract class curyAdjdBilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOAdjust.curyAdjdBilledAmt>
  {
  }

  public abstract class curyAdjgTransferredToChildrenAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOAdjust.curyAdjgTransferredToChildrenAmt>
  {
  }

  public abstract class adjTransferredToChildrenAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOAdjust.adjTransferredToChildrenAmt>
  {
  }

  public abstract class curyAdjdTransferredToChildrenAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOAdjust.curyAdjdTransferredToChildrenAmt>
  {
  }

  public abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOAdjust.curyDocBal>
  {
  }

  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOAdjust.docBal>
  {
  }

  public abstract class isCCPayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAdjust.isCCPayment>
  {
  }

  public abstract class paymentReleased : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAdjust.paymentReleased>
  {
  }

  public abstract class isCCAuthorized : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAdjust.isCCAuthorized>
  {
  }

  public abstract class isCCCaptured : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAdjust.isCCCaptured>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAdjust.voided>
  {
  }

  public abstract class blanketRecordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOAdjust.blanketRecordID>
  {
  }

  public abstract class blanketType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAdjust.blanketType>
  {
  }

  public abstract class blanketNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAdjust.blanketNbr>
  {
  }

  /// <summary>Orig. Transaction number (for customer refunds).</summary>
  public abstract class refTranExtNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAdjust.refTranExtNbr>
  {
  }

  /// <summary>
  /// ExternalRef for ecommerce - to track external payment refernce
  /// </summary>
  public abstract class externalRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAdjust.externalRef>
  {
  }

  /// <summary>
  /// When true - AuthorizeCCPayment action will be called automatically.
  /// </summary>
  public abstract class authorize : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAdjust.authorize>
  {
  }

  /// <summary>
  /// When true - CaptureCCPayment action will be called automatically.
  /// </summary>
  public abstract class capture : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAdjust.capture>
  {
  }

  /// <summary>
  /// When true - CreditCCPayment action will be called automatically.
  /// </summary>
  public abstract class refund : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAdjust.refund>
  {
  }

  /// <summary>
  /// When false - the validation of the link between the sales order lines (returns) and the original CC transaction will be skipped.
  /// </summary>
  public abstract class validateCCRefundOrigTransaction : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOAdjust.validateCCRefundOrigTransaction>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOAdjust.noteID>
  {
  }

  public abstract class isBalanceRecalculationRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOAdjust.isBalanceRecalculationRequired>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOAdjust.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOAdjust.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOAdjust.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOAdjust.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOAdjust.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOAdjust.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOAdjust.lastModifiedDateTime>
  {
  }

  public abstract class paymentMethodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAdjust.paymentMethodID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOAdjust.cashAccountID>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOAdjust.pMInstanceID>
  {
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOAdjust.processingCenterID>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAdjust.extRefNbr>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAdjust.docDesc>
  {
  }

  public abstract class curyOrigDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOAdjust.curyOrigDocAmt>
  {
  }

  public abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOAdjust.origDocAmt>
  {
  }

  public abstract class syncLock : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAdjust.syncLock>
  {
  }

  public abstract class syncLockReason : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOAdjust.syncLockReason>
  {
  }

  public abstract class newCard : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAdjust.newCard>
  {
  }

  public abstract class saveCard : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAdjust.saveCard>
  {
  }

  public abstract class pendingPayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOAdjust.pendingPayment>
  {
  }
}
