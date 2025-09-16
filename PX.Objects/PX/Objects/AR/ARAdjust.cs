// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARAdjust
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.MigrationMode;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.SO;
using PX.Objects.SO.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// The fact of application of one accounts receivable document to another,
/// which results in an adjustment of the balances of both documents. It can be either
/// an application of a <see cref="T:PX.Objects.AR.ARPayment">payment document</see> to an
/// <see cref="T:PX.Objects.AR.ARAdjust.ARInvoice">invoice document</see> (such as when a payment
/// closes an invoice), or an application of one <see cref="T:PX.Objects.AR.ARPayment">payment</see> document
/// to another, such as an application of a customer refund to a payment. The entities
/// of this type are mainly edited on the Payments and Applications (AR302000) form,
/// which corresponds to the <see cref="T:PX.Objects.AR.ARPaymentEntry" /> graph. They can also be edited
/// on the Applications tab of the Invoices and Memos (AR301000) form, which corresponds
/// to the <see cref="T:PX.Objects.AR.ARInvoiceEntry" /> graph.
/// </summary>
[PXPrimaryGraph(new Type[] {typeof (ARPaymentEntry)}, new Type[] {typeof (Select<ARPayment, Where<ARPayment.docType, Equal<Current<ARAdjust.adjgDocType>>, And<ARPayment.refNbr, Equal<Current<ARAdjust.adjgRefNbr>>>>>)})]
[PXCacheName("Applications")]
[DebuggerDisplay("{AdjdDocType}:{AdjdRefNbr}:{AdjdLineNbr} - {AdjgDocType}:{AdjgRefNbr}:{AdjNbr}, tstamp = {PX.Data.PXDBTimestampAttribute.ToString(tstamp)}")]
[Serializable]
public class ARAdjust : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAdjustment,
  IDocumentAdjustment,
  IAdjustmentAmount,
  IFinAdjust,
  ICreatePaymentAdjust
{
  protected int? _CustomerID;
  protected int? _AdjdCustomerID;
  protected 
  #nullable disable
  string _AdjgDocType;
  protected string _AdjgRefNbr;
  protected int? _AdjgBranchID;
  protected long? _AdjdCuryInfoID;
  protected string _AdjdOrderType;
  protected string _AdjdOrderNbr;
  protected int? _AdjdBranchID;
  protected int? _AdjNbr;
  protected string _AdjBatchNbr;
  protected int? _VoidAdjNbr;
  protected long? _AdjdOrigCuryInfoID;
  protected long? _AdjgCuryInfoID;
  protected DateTime? _AdjgDocDate;
  protected string _AdjgFinPeriodID;
  protected string _AdjgTranPeriodID;
  protected DateTime? _AdjdDocDate;
  protected string _AdjdFinPeriodID;
  protected string _AdjdTranPeriodID;
  protected Decimal? _CuryAdjgDiscAmt;
  protected Decimal? _CuryAdjgPPDAmt;
  protected Decimal? _CuryAdjgWOAmt;
  protected Decimal? _CuryAdjgAmt;
  protected Decimal? _AdjDiscAmt;
  protected Decimal? _AdjPPDAmt;
  protected Decimal? _CuryAdjdDiscAmt;
  protected Decimal? _CuryAdjdPPDAmt;
  protected Decimal? _AdjWOAmt;
  protected Decimal? _CuryAdjdWOAmt;
  protected Decimal? _AdjAmt;
  protected Decimal? _CuryAdjdAmt;
  protected Decimal? _CuryAdjdOrigAmt;
  protected Decimal? _RGOLAmt;
  protected bool? _Released;
  protected bool? _Hold;
  protected bool? _Voided;
  protected int? _AdjdARAcct;
  protected int? _AdjdARSub;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected Guid? _NoteID;
  protected Decimal? _AdjdCuryRate;
  protected Decimal? _CuryDocBal;
  protected Decimal? _DocBal;
  protected Decimal? _CuryDiscBal;
  protected Decimal? _DiscBal;
  protected Decimal? _CuryWOBal;
  protected Decimal? _WOBal;
  protected string _WriteOffReasonCode;
  protected bool? _AdjdHasPPDTaxes;
  protected bool? _PendingPPD;

  /// <summary>
  /// Indicates whether the record is selected for processing.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected", Enabled = false)]
  public virtual bool? Selected { get; set; }

  [PXDBInt]
  [PXDBDefault(typeof (ARRegister.customerID))]
  [PXUIField]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDefault]
  [Customer(Visible = false, Enabled = false)]
  public virtual int? AdjdCustomerID
  {
    get => this._AdjdCustomerID;
    set => this._AdjdCustomerID = value;
  }

  /// <summary>
  /// Adjustment type - an incoming or outgoing
  /// adjustment. Controlled by specific BLCs, e.g.
  /// filled out in application delegate in <see cref="T:PX.Objects.AR.ARInvoiceEntry" />.
  /// </summary>
  [PXString(1, IsFixed = true)]
  [ARAdjust.adjType.List]
  [PXDefault("D")]
  public virtual string AdjType { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [PXDBDefault(typeof (ARRegister.docType))]
  [PXUIField]
  public virtual string AdjgDocType
  {
    get => this._AdjgDocType;
    set => this._AdjgDocType = value;
  }

  [PXString(3, IsFixed = true)]
  [ARDocType.PrintList]
  [PXUIField]
  public virtual string PrintAdjgDocType
  {
    get => this._AdjgDocType;
    set
    {
    }
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (ARRegister.refNbr))]
  [PXUIField]
  [PXParent(typeof (Select<ARRegister, Where<ARRegister.docType, Equal<Current<ARAdjust.adjgDocType>>, And<ARRegister.refNbr, Equal<Current<ARAdjust.adjgRefNbr>>, And<Current<ARAdjust.released>, NotEqual<True>>>>>))]
  [PXParent(typeof (Select<ARPaymentTotals, Where<ARPaymentTotals.docType, Equal<Current<ARAdjust.adjgDocType>>, And<ARPaymentTotals.refNbr, Equal<Current<ARAdjust.adjgRefNbr>>>>>), ParentCreate = true, LeaveChildren = true)]
  public virtual string AdjgRefNbr
  {
    get => this._AdjgRefNbr;
    set => this._AdjgRefNbr = value;
  }

  [Branch(typeof (ARRegister.branchID), null, true, true, true)]
  public virtual int? AdjgBranchID
  {
    get => this._AdjgBranchID;
    set => this._AdjgBranchID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (ARRegister.curyInfoID))]
  public virtual long? AdjdCuryInfoID
  {
    get => this._AdjdCuryInfoID;
    set => this._AdjdCuryInfoID = value;
  }

  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string AdjdCuryID { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
  [PXDefault("INV")]
  [PXUIField]
  [ARInvoiceType.AdjdList]
  public virtual string AdjdDocType { get; set; }

  [PXString(3, IsFixed = true)]
  [ARDocType.PrintList]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual string PrintAdjdDocType
  {
    get => this.AdjdDocType;
    set
    {
    }
  }

  [PXString(3, IsFixed = true)]
  [ARDocType.List]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual string HistoryAdjdDocType
  {
    get => this.AdjdDocType;
    set
    {
    }
  }

  [PXString(3, IsFixed = true, InputMask = "")]
  [PXUIField(DisplayName = "Doc. Type")]
  [ARInvoiceType.AdjList(ExclusiveValues = true)]
  public virtual string DisplayDocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [ARInvoiceType.AdjdRefNbr(typeof (Search2<ARAdjust.ARInvoice.refNbr, LeftJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<ARAdjust.ARInvoice.docType>, And<ARAdjust.adjdRefNbr, Equal<ARAdjust.ARInvoice.refNbr>, And<ARAdjust.released, NotEqual<True>, And<ARAdjust.voided, NotEqual<True>, And<Where<ARAdjust.adjgDocType, NotEqual<Current<ARRegister.docType>>, Or<ARAdjust.adjgRefNbr, NotEqual<Current<ARRegister.refNbr>>>>>>>>>, LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<ARAdjust.ARInvoice.docType>, And<ARAdjust2.adjgRefNbr, Equal<ARAdjust.ARInvoice.refNbr>, And<ARAdjust2.released, NotEqual<True>, And<ARAdjust2.voided, NotEqual<True>>>>>, LeftJoin<Customer, On<ARAdjust.ARInvoice.customerID, Equal<Customer.bAccountID>>, LeftJoin<PX.Objects.SO.SOInvoice, On<ARAdjust.ARInvoice.docType, Equal<PX.Objects.SO.SOInvoice.docType>, And<ARAdjust.ARInvoice.refNbr, Equal<PX.Objects.SO.SOInvoice.refNbr>>>>>>>, Where<ARAdjust.ARInvoice.docType, Equal<Optional<ARAdjust.adjdDocType>>, And2<Where<ARAdjust.ARInvoice.released, Equal<True>, Or<ARRegister.origModule, Equal<BatchModule.moduleSO>, And<ARAdjust.ARInvoice.docType, NotEqual<ARDocType.creditMemo>>>>, And<ARAdjust.ARInvoice.openDoc, Equal<True>, And<ARRegister.hold, Equal<False>, And<ARAdjust.adjgRefNbr, IsNull, And<ARAdjust2.adjdRefNbr, IsNull, And2<Where<ARAdjust.ARInvoice.docType, NotEqual<ARDocType.prepaymentInvoice>, Or2<Where<ARRegister.pendingPayment, NotEqual<True>, And<Current<ARRegister.docType>, Equal<ARDocType.refund>, Or<Current<ARRegister.docType>, Equal<ARDocType.voidRefund>>>>, Or<ARRegister.pendingPayment, Equal<True>, And<Current<ARRegister.docType>, NotEqual<ARDocType.refund>, And<Current<ARRegister.docType>, NotEqual<ARDocType.voidRefund>>>>>>, And<ARAdjust.ARInvoice.customerID, In2<Search<PX.Objects.AR.Override.BAccount.bAccountID, Where<PX.Objects.AR.Override.BAccount.bAccountID, Equal<Optional<ARRegister.customerID>>, Or<PX.Objects.AR.Override.BAccount.consolidatingBAccountID, Equal<Optional<ARRegister.customerID>>>>>>, And2<Where<ARAdjust.ARInvoice.pendingPPD, NotEqual<True>, Or<Current<ARRegister.pendingPPD>, Equal<True>>>, And<Where<Current<ARSetup.migrationMode>, NotEqual<True>, Or<ARAdjust.ARInvoice.isMigratedRecord, Equal<Current<ARRegister.isMigratedRecord>>>>>>>>>>>>>>>), Filterable = true)]
  [CopyChildLink(typeof (ARPaymentTotals.invoiceCntr), typeof (ARAdjust.curyAdjdAmt), new Type[] {typeof (ARAdjust.adjdDocType), typeof (ARAdjust.adjdRefNbr)}, new Type[] {typeof (ARPaymentTotals.adjdDocType), typeof (ARPaymentTotals.adjdRefNbr)})]
  public virtual string AdjdRefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXDefault(typeof (Switch<Case<Where<Selector<ARAdjust.adjdRefNbr, ARAdjust.ARInvoice.paymentsByLinesAllowed>, NotEqual<True>>, int0>, Null>))]
  [ARInvoiceType.AdjdLineNbr]
  [ARInvoiceType.AdjdLineNbrRestrictor(typeof (Where<ARAdjust.ARInvoice.paymentsByLinesAllowed, Equal<True>, And<Where<ARTran.curyTranBal, NotEqual<decimal0>, Or<ARAdjust.ARInvoice.retainageApply, Equal<True>, And<ARAdjust.ARInvoice.retainageUnreleasedAmt, Equal<decimal0>, And<ARAdjust.ARInvoice.retainageReleased, Equal<decimal0>>>>>>>), "'{0}' cannot be found in the system.", new Type[] {typeof (ARTran.lineNbr)})]
  public virtual int? AdjdLineNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Order Type")]
  [PXSelector(typeof (Search<SOOrderType.orderType>))]
  public virtual string AdjdOrderType
  {
    get => this._AdjdOrderType;
    set => this._AdjdOrderType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Optional<ARAdjust.adjdOrderType>>>>), Filterable = true)]
  [PXParent(typeof (Select<SOAdjust, Where<SOAdjust.adjdOrderType, Equal<Current<ARAdjust.adjdOrderType>>, And<SOAdjust.adjdOrderNbr, Equal<Current<ARAdjust.adjdOrderNbr>>, And<SOAdjust.adjgDocType, Equal<Current<ARAdjust.adjgDocType>>, And<SOAdjust.adjgRefNbr, Equal<Current<ARAdjust.adjgRefNbr>>>>>>>), LeaveChildren = true)]
  public virtual string AdjdOrderNbr
  {
    get => this._AdjdOrderNbr;
    set => this._AdjdOrderNbr = value;
  }

  [Branch(null, null, true, true, false, Enabled = false)]
  public virtual int? AdjdBranchID
  {
    get => this._AdjdBranchID;
    set => this._AdjdBranchID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXDefault(typeof (ARPayment.adjCntr))]
  public virtual int? AdjNbr
  {
    get => this._AdjNbr;
    set => this._AdjNbr = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  public virtual string AdjBatchNbr
  {
    get => this._AdjBatchNbr;
    set => this._AdjBatchNbr = value;
  }

  [PXDBInt]
  public virtual int? VoidAdjNbr
  {
    get => this._VoidAdjNbr;
    set => this._VoidAdjNbr = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (ARRegister.curyInfoID))]
  public virtual long? AdjdOrigCuryInfoID
  {
    get => this._AdjdOrigCuryInfoID;
    set => this._AdjdOrigCuryInfoID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (ARRegister.curyInfoID))]
  public virtual long? AdjgCuryInfoID
  {
    get => this._AdjgCuryInfoID;
    set => this._AdjgCuryInfoID = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (ARPayment.adjDate))]
  public virtual DateTime? AdjgDocDate
  {
    get => this._AdjgDocDate;
    set => this._AdjgDocDate = value;
  }

  [FinPeriodID(null, typeof (ARAdjust.adjgBranchID), null, null, null, null, true, false, null, typeof (ARAdjust.adjgTranPeriodID), typeof (ARPayment.adjTranPeriodID), true, true)]
  [PXUIField(DisplayName = "Application Period", Enabled = false)]
  public virtual string AdjgFinPeriodID
  {
    get => this._AdjgFinPeriodID;
    set => this._AdjgFinPeriodID = value;
  }

  [PeriodID(null, null, null, true)]
  public virtual string AdjgTranPeriodID
  {
    get => this._AdjgTranPeriodID;
    set => this._AdjgTranPeriodID = value;
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? AdjdDocDate
  {
    get => this._AdjdDocDate;
    set => this._AdjdDocDate = value;
  }

  [FinPeriodID(null, typeof (ARAdjust.adjdBranchID), null, null, null, null, true, false, null, typeof (ARAdjust.adjdTranPeriodID), null, true, true)]
  [PXUIField(DisplayName = "Post Period", Enabled = false)]
  public virtual string AdjdFinPeriodID
  {
    get => this._AdjdFinPeriodID;
    set => this._AdjdFinPeriodID = value;
  }

  [PeriodID(null, null, null, true)]
  public virtual string AdjdTranPeriodID
  {
    get => this._AdjdTranPeriodID;
    set => this._AdjdTranPeriodID = value;
  }

  [PXDBCurrency(typeof (ARAdjust.adjgCuryInfoID), typeof (ARAdjust.adjDiscAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryAdjgDiscAmt
  {
    get => this._CuryAdjgDiscAmt;
    set => this._CuryAdjgDiscAmt = value;
  }

  /// <summary>
  /// The cash discount amount displayed for the document.
  /// Given in the <see cref="!:CuryID"> currency of the adjusting document</see>.
  /// </summary>
  [PXDBCurrency(typeof (ARAdjust.adjgCuryInfoID), typeof (ARAdjust.adjPPDAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryAdjgPPDAmt
  {
    get => this._CuryAdjgPPDAmt;
    set => this._CuryAdjgPPDAmt = value;
  }

  [PXDBCurrency(typeof (ARAdjust.adjgCuryInfoID), typeof (ARAdjust.adjWOAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXFormula(null, typeof (SumCalc<ARPayment.curyWOAmt>))]
  public virtual Decimal? CuryAdjgWOAmt
  {
    get => this._CuryAdjgWOAmt;
    set => this._CuryAdjgWOAmt = value;
  }

  public virtual Decimal? CuryAdjgWhTaxAmt
  {
    get => this._CuryAdjgWOAmt;
    set => this._CuryAdjgWOAmt = value;
  }

  [PXDBCurrency(typeof (ARAdjust.adjgCuryInfoID), typeof (ARAdjust.adjAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXFormula(null, typeof (SumCalc<SOAdjust.curyAdjgBilledAmt>))]
  public virtual Decimal? CuryAdjgAmt
  {
    get => this._CuryAdjgAmt;
    set => this._CuryAdjgAmt = value;
  }

  /// <summary>
  /// The signed foreign currency value of <see cref="P:PX.Objects.AR.ARAdjust.CuryAdjgAmt" /> depending on <see cref="P:PX.Objects.AR.ARAdjust.AdjdDocType" />
  /// </summary>
  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<ARAdjust.adjdDocType, In3<ARAdjust.curyAdjgSignedAmt.DocTypesWithPositiveSign>>, ARAdjust.curyAdjgAmt, Case<Where<ARAdjust.adjdDocType, In3<ARAdjust.curyAdjgSignedAmt.DocTypesWithNegativeSign>>, Minus<ARAdjust.curyAdjgAmt>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? CuryAdjgSignedAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjDiscAmt
  {
    get => this._AdjDiscAmt;
    set => this._AdjDiscAmt = value;
  }

  /// <summary>
  /// The cash discount amount displayed for the document.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjPPDAmt
  {
    get => this._AdjPPDAmt;
    set => this._AdjPPDAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryAdjdDiscAmt
  {
    get => this._CuryAdjdDiscAmt;
    set => this._CuryAdjdDiscAmt = value;
  }

  /// <summary>
  /// The cash discount amount displayed for the document.
  /// Given in the <see cref="!:CuryID"> currency of the adjusted document</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryAdjdPPDAmt
  {
    get => this._CuryAdjdPPDAmt;
    set => this._CuryAdjdPPDAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjWOAmt
  {
    get => this._AdjWOAmt;
    set => this._AdjWOAmt = value;
  }

  public virtual Decimal? AdjWhTaxAmt
  {
    get => this._AdjWOAmt;
    set => this._AdjWOAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjdWOAmt
  {
    get => this._CuryAdjdWOAmt;
    set => this._CuryAdjdWOAmt = value;
  }

  public virtual Decimal? CuryAdjdWhTaxAmt
  {
    get => this._CuryAdjdWOAmt;
    set => this._CuryAdjdWOAmt = value;
  }

  [PXDBDecimal(4)]
  [PXFormula(null, typeof (SumCalc<SOAdjust.adjBilledAmt>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AdjAmt
  {
    get => this._AdjAmt;
    set => this._AdjAmt = value;
  }

  /// <summary>
  /// The signed base currency value of <see cref="P:PX.Objects.AR.ARAdjust.AdjAmt" /> depending on <see cref="P:PX.Objects.AR.ARAdjust.AdjdDocType" />
  /// </summary>
  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<ARAdjust.adjdDocType, In3<ARAdjust.curyAdjgSignedAmt.DocTypesWithPositiveSign>>, ARAdjust.adjAmt, Case<Where<ARAdjust.adjdDocType, In3<ARAdjust.curyAdjgSignedAmt.DocTypesWithNegativeSign>>, Minus<ARAdjust.adjAmt>>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? AdjSignedAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(null, typeof (SumCalc<SOAdjust.curyAdjdBilledAmt>))]
  public virtual Decimal? CuryAdjdAmt
  {
    get => this._CuryAdjdAmt;
    set => this._CuryAdjdAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAdjdOrigAmt
  {
    get => this._CuryAdjdOrigAmt;
    set => this._CuryAdjdOrigAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RGOLAmt
  {
    get => this._RGOLAmt;
    set => this._RGOLAmt = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  [Account]
  [PXDefault]
  public virtual int? AdjdARAcct
  {
    get => this._AdjdARAcct;
    set => this._AdjdARAcct = value;
  }

  [SubAccount]
  [PXDefault]
  public virtual int? AdjdARSub
  {
    get => this._AdjdARSub;
    set => this._AdjdARSub = value;
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

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBGuid(false)]
  public virtual Guid? InvoiceID { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? PaymentID { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? MemoID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDecimal(8)]
  [PXUIField]
  public virtual Decimal? AdjdCuryRate
  {
    get => this._AdjdCuryRate;
    set => this._AdjdCuryRate = value;
  }

  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (ARAdjust.adjgCuryInfoID), typeof (ARAdjust.origDocAmt), BaseCalc = false)]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  [PXDecimal(4)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigDocAmt { get; set; }

  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (ARAdjust.adjgCuryInfoID), typeof (ARAdjust.docBal), BaseCalc = false)]
  [PXUIField]
  public virtual Decimal? CuryDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  public virtual Decimal? CuryPayDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  [PXDecimal(4)]
  public virtual Decimal? DocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  public virtual Decimal? PayDocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  [PXCurrency(typeof (ARAdjust.adjgCuryInfoID), typeof (ARAdjust.discBal), BaseCalc = false)]
  [PXUIField]
  public virtual Decimal? CuryDiscBal
  {
    get => this._CuryDiscBal;
    set => this._CuryDiscBal = value;
  }

  public virtual Decimal? CuryPayDiscBal
  {
    get => this._CuryDiscBal;
    set => this._CuryDiscBal = value;
  }

  [PXDecimal(4)]
  public virtual Decimal? DiscBal
  {
    get => this._DiscBal;
    set => this._DiscBal = value;
  }

  public virtual Decimal? PayDiscBal
  {
    get => this._DiscBal;
    set => this._DiscBal = value;
  }

  [PXCurrency(typeof (ARAdjust.adjgCuryInfoID), typeof (ARAdjust.wOBal), BaseCalc = false)]
  [PXUnboundDefault]
  [PXUIField]
  public virtual Decimal? CuryWOBal
  {
    get => this._CuryWOBal;
    set => this._CuryWOBal = value;
  }

  public virtual Decimal? CuryWhTaxBal
  {
    get => this._CuryWOBal;
    set => this._CuryWOBal = value;
  }

  [PXDecimal(4)]
  public virtual Decimal? WOBal
  {
    get => this._WOBal;
    set => this._WOBal = value;
  }

  public virtual Decimal? WhTaxBal
  {
    get => this._WOBal;
    set => this._WOBal = value;
  }

  [PXFormula(typeof (Switch<Case<Where<ARAdjust.adjdDocType, NotEqual<ARDocType.creditMemo>>, Current<ARSetup.balanceWriteOff>>>))]
  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.creditWriteOff>, Or<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.balanceWriteOff>>>>))]
  [PXUIField]
  [PXForeignReference(typeof (Field<ARAdjust.writeOffReasonCode>.IsRelatedTo<PX.Objects.CS.ReasonCode.reasonCodeID>))]
  public virtual string WriteOffReasonCode
  {
    get => this._WriteOffReasonCode;
    set => this._WriteOffReasonCode = value;
  }

  [PXBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? VoidAppl
  {
    [PXDependsOnFields(new Type[] {typeof (ARAdjust.adjgDocType)})] get
    {
      return new bool?(ARPaymentType.VoidAppl(this.AdjgDocType));
    }
    set
    {
      if (!value.Value || ARPaymentType.VoidAppl(this.AdjgDocType))
        return;
      this.AdjgDocType = ARPaymentType.GetVoidingARDocType(this.AdjgDocType);
      this.Voided = new bool?(true);
    }
  }

  [PXBool]
  [PXDependsOnFields(new Type[] {typeof (ARAdjust.adjgDocType)})]
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

  [PXDependsOnFields(new Type[] {typeof (ARAdjust.adjgDocType), typeof (ARAdjust.adjdDocType)})]
  public virtual Decimal? AdjgBalSign
  {
    get
    {
      return new Decimal?(this.AdjgDocType == "PMT" && this.AdjdDocType == "CRM" || this.AdjgDocType == "RPM" && this.AdjdDocType == "CRM" || this.AdjgDocType == "PPM" && this.AdjdDocType == "CRM" ? -1M : 1M);
    }
    set
    {
    }
  }

  [PXDependsOnFields(new Type[] {typeof (ARAdjust.adjgDocType), typeof (ARAdjust.adjdDocType)})]
  public virtual Decimal? AdjgGLSign
  {
    get
    {
      return !(this.AdjdDocType == "PPI") || !(this.AdjgDocType == "REF") && !(this.AdjgDocType == "VRF") ? ARDocType.SignAmount(this.AdjdDocType) : new Decimal?(-1M);
    }
    set
    {
    }
  }

  [PXDependsOnFields(new Type[] {typeof (ARAdjust.adjgDocType), typeof (ARAdjust.adjdDocType)})]
  public virtual Decimal? AdjgTBSign
  {
    get
    {
      if (this.AdjdDocType == "PPI")
      {
        Decimal num = -1M;
        Decimal? nullable = ARDocType.SignBalance(this.AdjgDocType);
        return !nullable.HasValue ? new Decimal?() : new Decimal?(num * nullable.GetValueOrDefault());
      }
      if (!this.IsSelfAdjustment())
        return ARDocType.SignBalance(this.AdjdDocType);
      Decimal num1 = -1M;
      Decimal? nullable1 = ARDocType.SignBalance(this.AdjgDocType);
      return !nullable1.HasValue ? new Decimal?() : new Decimal?(num1 * nullable1.GetValueOrDefault());
    }
  }

  [PXDependsOnFields(new Type[] {typeof (ARAdjust.adjgDocType), typeof (ARAdjust.adjdDocType)})]
  public virtual Decimal? AdjdTBSign
  {
    get
    {
      if (this.IsSelfAdjustment())
        return new Decimal?(0M);
      return (!(this.AdjgDocType == "PMT") || !(this.AdjdDocType == "CRM")) && (!(this.AdjgDocType == "RPM") || !(this.AdjdDocType == "CRM")) && (!(this.AdjgDocType == "PPM") || !(this.AdjdDocType == "CRM")) ? ARDocType.SignBalance(this.AdjgDocType) : new Decimal?(1M);
    }
  }

  public virtual Decimal? CuryPayWhTaxBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public virtual Decimal? PayWhTaxBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public virtual Decimal? SignedRGOLAmt
  {
    [PXDependsOnFields(new Type[] {typeof (ARAdjust.reverseGainLoss), typeof (ARAdjust.rGOLAmt)})] get
    {
      if (!this.ReverseGainLoss.Value)
        return this._RGOLAmt;
      Decimal? rgolAmt = this._RGOLAmt;
      return !rgolAmt.HasValue ? new Decimal?() : new Decimal?(-rgolAmt.GetValueOrDefault());
    }
  }

  [PXDependsOnFields(new Type[] {typeof (ARAdjust.adjgDocType), typeof (ARAdjust.adjdDocType)})]
  public virtual Decimal? AdjdBalSign
  {
    get
    {
      return new Decimal?(this.AdjgDocType == "PMT" && this.AdjdDocType == "CRM" || this.AdjgDocType == "RPM" && this.AdjdDocType == "CRM" || this.AdjgDocType == "PPM" && this.AdjdDocType == "CRM" ? -1M : 1M);
    }
    set
    {
    }
  }

  /// <summary>
  /// The reference number of the vat adjustment document, which is generated on the Generate AR Tax Adjustments (AR504500) form.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:ARInvoice.RefNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.AR.ARInvoice.refNbr, Where<PX.Objects.AR.ARInvoice.docType, Equal<ARDocType.creditMemo>, And<PX.Objects.AR.ARInvoice.pendingPPD, Equal<True>>>>))]
  [PXUIField(DisplayName = "Tax Adjustment", Enabled = false, Visible = false)]
  public virtual string PPDVATAdjRefNbr { get; set; }

  /// <summary>
  /// The doc type of the adjustment, which is generated on the Generate AR Tax Adjustments (AR504500) form.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="T:PX.Objects.AR.ARAdjust.ARInvoice.docType" /> field.
  /// </value>
  [PXDBString(3, IsFixed = true, InputMask = "")]
  [ARDocType.List]
  public virtual string PPDVATAdjDocType { get; set; }

  /// <summary>
  /// The description of the adjustment, which is generated on the Generate AR Tax Adjustments (AR504500) form.
  /// This field is needed for showing on the Payments and Applications (AR302000) screen like a hyperlink. For example - "Credit Memo. AR012053".
  /// </summary>
  /// <value>
  /// Value is <see cref="P:PX.Objects.AR.ARAdjust.PPDVATAdjDocType" /> + <see cref="P:PX.Objects.AR.ARAdjust.PPDVATAdjRefNbr" /> field.
  /// </value>
  [PXString(40, InputMask = "")]
  [PXUIField(DisplayName = "Tax Adjustment", Enabled = false, Visible = false)]
  public virtual string PPDVATAdjDescription { get; set; }

  /// <summary>
  /// When <c>true</c>, indicates that the linked adjusted document has taxes, that are reduces cash discount taxable amount on early payment.
  /// </summary>
  [PXDBBool]
  [PXDefault]
  [PXFormula(typeof (IsNull<Selector<ARAdjust.adjdRefNbr, ARAdjust.ARInvoice.hasPPDTaxes>, False>))]
  public virtual bool? AdjdHasPPDTaxes
  {
    get => this._AdjdHasPPDTaxes;
    set => this._AdjdHasPPDTaxes = value;
  }

  /// <summary>
  /// A Boolean value that indicates (if it is <see langword="true" />) that the linked adjusted document has been paid in full and
  /// to close the document, you need to apply the cash discount by generating a credit memo on the Generate AR Tax Adjustments (AR504500) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Subject to Tax Adjustment", Enabled = false, Visible = false)]
  public virtual bool? PendingPPD
  {
    get => this._PendingPPD;
    set => this._PendingPPD = value;
  }

  [PXDBDate]
  public virtual DateTime? StatementDate { get; set; }

  /// <summary>
  /// The "Tax Doc. Nbr" of the tax transaction, generated on the "Recognize Output/Input VAT" (TX503000/TX503500) form.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxTran.TaxInvoiceNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Doc. Nbr", Enabled = false, Visible = false)]
  public virtual string TaxInvoiceNbr { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the record has been created
  /// in migration mode without affecting GL module.
  /// </summary>
  [MigratedRecord(typeof (ARSetup.migrationMode))]
  public virtual bool? IsMigratedRecord { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the application has been created
  /// for the migrated document to affect all needed balances.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsInitialApplication { get; set; }

  [PXDBBool]
  [DenormalizedFrom(new Type[] {typeof (ARPayment.isCCPayment), typeof (ARPayment.pendingProcessing), typeof (ARPayment.released), typeof (ARPayment.isCCAuthorized), typeof (ARPayment.isCCCaptured), typeof (ARPayment.isCCCaptureFailed)}, new Type[] {typeof (ARAdjust.isCCPayment), typeof (ARAdjust.paymentPendingProcessing), typeof (ARAdjust.paymentReleased), typeof (ARAdjust.isCCAuthorized), typeof (ARAdjust.isCCCaptured), typeof (ARAdjust.paymentCaptureFailed)}, new object[] {false, false, false, false, false, false}, typeof (ARAdjust.adjgRefNbr))]
  public virtual bool? IsCCPayment { get; set; }

  [PXDBBool]
  public virtual bool? PaymentPendingProcessing { get; set; }

  [PXDBBool]
  public virtual bool? PaymentReleased { get; set; }

  [PXDBBool]
  public virtual bool? IsCCAuthorized { get; set; }

  [PXDBBool]
  public virtual bool? IsCCCaptured { get; set; }

  [PXDBBool]
  public virtual bool? PaymentCaptureFailed { get; set; }

  /// <summary>
  /// Specifies (if set to <see langword="true" />) that the invoice has external taxes
  /// and have to recalculate the amount paid
  /// If the user manually adjust the amount, this flag's value is set to <see langword="false" />
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Recalculatable { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Reference Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.AR.Standalone.ARRegister.refNbr, Where<PX.Objects.AR.Standalone.ARRegister.docType, Equal<Current<ARAdjust.displayDocType>>>>), ValidateValue = false)]
  public virtual string DisplayRefNbr { get; set; }

  [PXInt]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD))]
  [PXUIField(DisplayName = "BranchID", Enabled = false)]
  public virtual int? DisplayBranchID { get; set; }

  [PXInt]
  [PXSelector(typeof (Customer.bAccountID), SubstituteKey = typeof (Customer.acctCD))]
  [PXUIField(DisplayName = "Customer", Enabled = false)]
  public virtual int? DisplayCustomerID { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Date", Enabled = false)]
  public virtual DateTime? DisplayDocDate { get; set; }

  [PXString(150, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string DisplayDocDesc { get; set; }

  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string DisplayCuryID { get; set; }

  [PXString(6, IsFixed = true)]
  [FinPeriodIDFormatting]
  [PXSelector(typeof (Search<MasterFinPeriod.finPeriodID>))]
  [PXUIField(DisplayName = "Post Period", Enabled = false)]
  public virtual string DisplayFinPeriodID { get; set; }

  [PXString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Status", Enabled = false)]
  [ARDocStatus.List]
  public virtual string DisplayStatus { get; set; }

  [PXLong]
  public virtual long? DisplayCuryInfoID { get; set; }

  [PXDecimal(4)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DisplayAdjAmt { get; set; }

  [PXCurrency(typeof (ARAdjust.displayCuryInfoID), typeof (ARAdjust.displayAdjAmt))]
  [PXUIField(DisplayName = "Amount Paid")]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DisplayCuryAmt { get; set; }

  [PXCurrency(typeof (ARAdjust.displayCuryInfoID), typeof (ARAdjust.adjPPDAmt), BaseCalc = false)]
  [PXUIField(DisplayName = "Cash Discount Taken", Enabled = false)]
  public virtual Decimal? DisplayCuryPPDAmt { get; set; }

  [PXCurrency(typeof (ARAdjust.displayCuryInfoID), typeof (ARAdjust.adjWOAmt), BaseCalc = false)]
  [PXUIField(DisplayName = "Write-Off Amount", Enabled = false)]
  public virtual Decimal? DisplayCuryWOAmt { get; set; }

  [PXString(3, IsFixed = true)]
  [ExtTransactionProcStatusCode.List]
  [PXUIField(DisplayName = "Proc. Status", Enabled = false)]
  public virtual string DisplayProcStatus { get; set; }

  string IDocumentAdjustment.Module => "AR";

  Decimal? IAdjustmentAmount.AdjThirdAmount
  {
    get => this.AdjWOAmt;
    set => this.AdjWOAmt = value;
  }

  Decimal? IAdjustmentAmount.CuryAdjdThirdAmount
  {
    get => this.CuryAdjdWOAmt;
    set => this.CuryAdjdWOAmt = value;
  }

  Decimal? IAdjustmentAmount.CuryAdjgThirdAmount
  {
    get => this.CuryAdjgWOAmt;
    set => this.CuryAdjgWOAmt = value;
  }

  public class PK : 
    PrimaryKeyOf<ARAdjust>.By<ARAdjust.adjgDocType, ARAdjust.adjgRefNbr, ARAdjust.adjNbr, ARAdjust.adjdDocType, ARAdjust.adjdRefNbr, ARAdjust.adjdLineNbr>
  {
    public static ARAdjust Find(
      PXGraph graph,
      string adjgDocType,
      string adjgRefNbr,
      int? adjNbr,
      string adjdDocType,
      string adjdRefNbr,
      int? adjdLineNbr)
    {
      return (ARAdjust) PrimaryKeyOf<ARAdjust>.By<ARAdjust.adjgDocType, ARAdjust.adjgRefNbr, ARAdjust.adjNbr, ARAdjust.adjdDocType, ARAdjust.adjdRefNbr, ARAdjust.adjdLineNbr>.FindBy(graph, (object) adjgDocType, (object) adjgRefNbr, (object) adjNbr, (object) adjdDocType, (object) adjdRefNbr, (object) adjdLineNbr, (PKFindOptions) 0);
    }
  }

  public static class FK
  {
    public class Payment : 
      PrimaryKeyOf<ARRegister>.By<ARRegister.docType, ARRegister.refNbr>.ForeignKeyOf<ARAdjust>.By<ARAdjust.adjgDocType, ARAdjust.adjgRefNbr>
    {
    }

    public class PaymentCurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARAdjust>.By<ARAdjust.adjgCuryInfoID>
    {
    }

    public class PaymentBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARAdjust>.By<ARAdjust.adjgBranchID>
    {
    }

    public class PaymentCustomer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARAdjust>.By<ARAdjust.customerID>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<ARRegister>.By<ARRegister.docType, ARRegister.refNbr>.ForeignKeyOf<ARAdjust>.By<ARAdjust.adjdDocType, ARAdjust.adjdRefNbr>
    {
    }

    public class InvoiceCurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARAdjust>.By<ARAdjust.adjdCuryInfoID>
    {
    }

    public class InvoiceBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARAdjust>.By<ARAdjust.adjdBranchID>
    {
    }

    public class InvoiceCustomer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARAdjust>.By<ARAdjust.adjdCustomerID>
    {
    }

    public class SOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<ARAdjust>.By<ARAdjust.adjdOrderType, ARAdjust.adjdOrderNbr>
    {
    }

    public class OrderCurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARAdjust>.By<ARAdjust.adjdOrigCuryInfoID>
    {
    }
  }

  public abstract class selected : IBqlField, IBqlOperand
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust.customerID>
  {
  }

  public abstract class adjdCustomerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust.adjdCustomerID>
  {
  }

  public abstract class adjType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.adjType>
  {
    /// <summary>
    /// Outgoing application of the current
    /// document to another document.
    /// </summary>
    public const string Adjusted = "D";
    /// <summary>
    /// Incoming application of another
    /// document to the current document.
    /// </summary>
    public const string Adjusting = "G";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "D", "G" }, new string[2]
        {
          "Adjusted",
          "Adjusting"
        })
      {
      }
    }

    public class adjusted : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARAdjust.adjType.adjusted>
    {
      public adjusted()
        : base("D")
      {
      }
    }

    public class adjusting : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARAdjust.adjType.adjusting>
    {
      public adjusting()
        : base("G")
      {
      }
    }
  }

  public abstract class adjgDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.adjgDocType>
  {
  }

  public abstract class printAdjgDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust.printAdjgDocType>
  {
  }

  public abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.adjgRefNbr>
  {
  }

  public abstract class adjgBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust.adjgBranchID>
  {
  }

  public abstract class adjdCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARAdjust.adjdCuryInfoID>
  {
  }

  public abstract class adjdCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.adjdCuryID>
  {
  }

  public abstract class adjdDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.adjdDocType>
  {
  }

  public abstract class printAdjdDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust.printAdjdDocType>
  {
  }

  public abstract class historyAdjdDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust.historyAdjdDocType>
  {
  }

  public abstract class displayDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.displayDocType>
  {
  }

  [PXHidden]
  [PXProjection(typeof (Select2<PX.Objects.AR.Standalone.ARRegister, LeftJoin<PX.Objects.AR.Standalone.ARInvoice, On<PX.Objects.AR.Standalone.ARInvoice.docType, Equal<PX.Objects.AR.Standalone.ARRegister.docType>, And<PX.Objects.AR.Standalone.ARInvoice.refNbr, Equal<PX.Objects.AR.Standalone.ARRegister.refNbr>>>>>))]
  [Serializable]
  public class ARInvoice : PX.Objects.AR.Standalone.ARRegister
  {
    [PXDBString(40, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Standalone.ARInvoice.invoiceNbr))]
    [PXUIField(DisplayName = "Customer Order Nbr.")]
    public virtual string InvoiceNbr { get; set; }

    public new abstract class docType : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust.ARInvoice.docType>
    {
    }

    public new abstract class refNbr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust.ARInvoice.refNbr>
    {
    }

    public new abstract class customerID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARAdjust.ARInvoice.customerID>
    {
    }

    public new abstract class released : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARAdjust.ARInvoice.released>
    {
    }

    public new abstract class pendingPPD : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARAdjust.ARInvoice.pendingPPD>
    {
    }

    public new abstract class hasPPDTaxes : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARAdjust.ARInvoice.hasPPDTaxes>
    {
    }

    public new abstract class openDoc : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARAdjust.ARInvoice.openDoc>
    {
    }

    public new abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARAdjust.ARInvoice.docDate>
    {
    }

    public new abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARAdjust.ARInvoice.finPeriodID>
    {
    }

    public new abstract class dueDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARAdjust.ARInvoice.dueDate>
    {
    }

    public abstract class invoiceNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARAdjust.ARInvoice.invoiceNbr>
    {
    }

    public new abstract class isMigratedRecord : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARAdjust.ARInvoice.isMigratedRecord>
    {
    }

    public new abstract class paymentsByLinesAllowed : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARAdjust.ARInvoice.paymentsByLinesAllowed>
    {
    }

    public new abstract class retainageApply : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARAdjust.ARInvoice.retainageApply>
    {
    }

    public new abstract class retainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARAdjust.ARInvoice.retainageUnreleasedAmt>
    {
    }

    public new abstract class retainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARAdjust.ARInvoice.retainageReleased>
    {
    }
  }

  public abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.adjdRefNbr>
  {
  }

  public abstract class adjdLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust.adjdLineNbr>
  {
  }

  public abstract class adjdOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.adjdOrderType>
  {
  }

  public abstract class adjdOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.adjdOrderNbr>
  {
  }

  public abstract class adjdBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust.adjdBranchID>
  {
  }

  public abstract class adjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust.adjNbr>
  {
  }

  public abstract class adjBatchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.adjBatchNbr>
  {
  }

  public abstract class voidAdjNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust.voidAdjNbr>
  {
  }

  public abstract class adjdOrigCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    ARAdjust.adjdOrigCuryInfoID>
  {
  }

  public abstract class adjgCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARAdjust.adjgCuryInfoID>
  {
  }

  public abstract class adjgDocDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARAdjust.adjgDocDate>
  {
  }

  public abstract class adjgFinPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.adjgFinPeriodID>
  {
  }

  public abstract class adjgTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust.adjgTranPeriodID>
  {
  }

  public abstract class adjdDocDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARAdjust.adjdDocDate>
  {
  }

  public abstract class adjdFinPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.adjdFinPeriodID>
  {
  }

  public abstract class adjdTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust.adjdTranPeriodID>
  {
  }

  public abstract class curyAdjgDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjust.curyAdjgDiscAmt>
  {
  }

  public abstract class curyAdjgPPDAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.curyAdjgPPDAmt>
  {
  }

  public abstract class curyAdjgWOAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.curyAdjgWOAmt>
  {
  }

  public abstract class curyAdjgAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.curyAdjgAmt>
  {
  }

  public abstract class curyAdjgSignedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjust.curyAdjgSignedAmt>
  {
    public class DocTypesWithPositiveSign : IBqlConstants
    {
      public IEnumerable<object> GetValues(PXGraph graph)
      {
        return (IEnumerable<object>) ((IEnumerable<string>) ARDocType.Values).Where<string>((Func<string, bool>) (v =>
        {
          Decimal? nullable = ARDocType.SignBalance(v);
          Decimal num = 1M;
          return nullable.GetValueOrDefault() == num & nullable.HasValue;
        })).ToArray<string>();
      }
    }

    public class DocTypesWithNegativeSign : IBqlConstants
    {
      public IEnumerable<object> GetValues(PXGraph graph)
      {
        return (IEnumerable<object>) ((IEnumerable<string>) ARDocType.Values).Where<string>((Func<string, bool>) (v =>
        {
          Decimal? nullable = ARDocType.SignBalance(v);
          Decimal num = -1M;
          return nullable.GetValueOrDefault() == num & nullable.HasValue;
        })).ToArray<string>();
      }
    }
  }

  public abstract class adjDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.adjDiscAmt>
  {
  }

  public abstract class adjPPDAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.adjPPDAmt>
  {
  }

  public abstract class curyAdjdDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjust.curyAdjdDiscAmt>
  {
  }

  public abstract class curyAdjdPPDAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.curyAdjdPPDAmt>
  {
  }

  public abstract class adjWOAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.adjWOAmt>
  {
  }

  public abstract class curyAdjdWOAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.curyAdjdWOAmt>
  {
  }

  public abstract class adjAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.adjAmt>
  {
  }

  public abstract class adjSignedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.adjSignedAmt>
  {
  }

  public abstract class curyAdjdAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.curyAdjdAmt>
  {
  }

  public abstract class curyAdjdOrigAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjust.curyAdjdOrigAmt>
  {
  }

  public abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.rGOLAmt>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust.released>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust.hold>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust.voided>
  {
  }

  public abstract class adjdARAcct : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust.adjdARAcct>
  {
  }

  public abstract class adjdARSub : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust.adjdARSub>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARAdjust.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARAdjust.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARAdjust.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARAdjust.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARAdjust.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARAdjust.noteID>
  {
  }

  public abstract class invoiceID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARAdjust.invoiceID>
  {
  }

  public abstract class paymentID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARAdjust.paymentID>
  {
  }

  public abstract class memoID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARAdjust.memoID>
  {
  }

  public abstract class adjdCuryRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.adjdCuryRate>
  {
  }

  public abstract class curyOrigDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.curyOrigDocAmt>
  {
  }

  public abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.origDocAmt>
  {
  }

  public abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.curyDocBal>
  {
  }

  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.docBal>
  {
  }

  public abstract class curyDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.curyDiscBal>
  {
  }

  public abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.discBal>
  {
  }

  public abstract class curyWOBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.curyWOBal>
  {
  }

  public abstract class wOBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.wOBal>
  {
  }

  public abstract class writeOffReasonCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust.writeOffReasonCode>
  {
  }

  public abstract class voidAppl : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust.voidAppl>
  {
  }

  public abstract class reverseGainLoss : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust.reverseGainLoss>
  {
  }

  public abstract class adjgBalSign : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.adjgBalSign>
  {
  }

  public abstract class adjgGLSign : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.adjgGLSign>
  {
  }

  public abstract class adjgTBSign : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.adjgTBSign>
  {
  }

  public abstract class adjdTBSign : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.adjdTBSign>
  {
  }

  public abstract class adjdBalSign : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.adjdBalSign>
  {
  }

  public abstract class pPDVATAdjRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.pPDVATAdjRefNbr>
  {
  }

  public abstract class pPDVATAdjDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust.pPDVATAdjDocType>
  {
  }

  public abstract class pPDVATAdjDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust.pPDVATAdjDescription>
  {
  }

  public abstract class adjdHasPPDTaxes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust.adjdHasPPDTaxes>
  {
  }

  public abstract class pendingPPD : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust.pendingPPD>
  {
  }

  public abstract class statementDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARAdjust.statementDate>
  {
  }

  public abstract class taxInvoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.taxInvoiceNbr>
  {
  }

  public abstract class isMigratedRecord : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust.isMigratedRecord>
  {
  }

  public abstract class isInitialApplication : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARAdjust.isInitialApplication>
  {
  }

  public abstract class isCCPayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust.isCCPayment>
  {
  }

  public abstract class paymentPendingProcessing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARAdjust.paymentPendingProcessing>
  {
  }

  public abstract class paymentReleased : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust.paymentReleased>
  {
  }

  public abstract class isCCAuthorized : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust.isCCAuthorized>
  {
  }

  public abstract class isCCCaptured : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust.isCCCaptured>
  {
  }

  public abstract class paymentCaptureFailed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARAdjust.paymentCaptureFailed>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARAdjust.Recalculatable" />
  public abstract class recalculatable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjust.recalculatable>
  {
  }

  /// <summary>
  /// These fields are required to display two-way (incoming and outgoing)
  /// applications in a same grid: e.g. displaying the adjusting document
  /// number in case of an incoming application, and adjusted document number
  /// in case of an outgoing application. The fields are controlled by specific
  /// BLCs, e.g. filled out in delegates or by formula.
  /// </summary>
  public abstract class displayRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.displayRefNbr>
  {
  }

  public abstract class displayBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust.displayBranchID>
  {
  }

  public abstract class displayCustomerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARAdjust.displayCustomerID>
  {
  }

  public abstract class displayDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARAdjust.displayDocDate>
  {
  }

  public abstract class displayDocDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.displayDocDesc>
  {
  }

  public abstract class displayCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.displayCuryID>
  {
  }

  public abstract class displayFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust.displayFinPeriodID>
  {
  }

  public abstract class displayStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjust.displayStatus>
  {
  }

  public abstract class displayCuryInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARAdjust.displayCuryInfoID>
  {
  }

  public abstract class displayAdjAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.displayAdjAmt>
  {
  }

  public abstract class displayCuryAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARAdjust.displayCuryAmt>
  {
  }

  public abstract class displayCuryPPDAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjust.displayCuryPPDAmt>
  {
  }

  public abstract class displayCuryWOAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjust.displayCuryWOAmt>
  {
  }

  public abstract class displayProcStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjust.displayProcStatus>
  {
  }
}
