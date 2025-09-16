// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPayment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.Standalone;
using PX.Objects.CA;
using PX.Objects.CC;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Interfaces;
using PX.Objects.GL;
using PX.Objects.GL.Descriptor;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// Represents an Accounts Receivable payment or a payment-like document, which can
/// have one of the types defined by <see cref="T:PX.Objects.AR.ARPaymentType.ListAttribute" />, such
/// as credit memo, customer refund, or balance write-off. The entities of this type
/// are edited on the Payments and Applications (AR302000) form, which corresponds
/// to the <see cref="T:PX.Objects.AR.ARPaymentEntry" /> graph.
/// </summary>
/// <remarks>
/// Credit memo, cash sale and cash return documents consist of both an
/// <see cref="T:PX.Objects.AR.ARInvoice">invoice</see> record and a corresponding
/// <see cref="T:PX.Objects.AR.ARPayment">payment</see> record.
/// </remarks>
[PXCacheName("AR Payment")]
[PXTable]
[PXSubstitute(GraphType = typeof (ARPaymentEntry))]
[PXPrimaryGraph(new System.Type[] {typeof (ARCashSaleEntry), typeof (ARPaymentEntry)}, new System.Type[] {typeof (Select<ARCashSale, Where2<Where<ARCashSale.docType, Equal<ARDocType.cashSale>, Or<ARRegister.docType, Equal<ARDocType.cashReturn>>>, And<ARCashSale.docType, Equal<Current<ARPayment.docType>>, And<ARCashSale.refNbr, Equal<Current<ARPayment.refNbr>>>>>>), typeof (Select<ARPayment, Where<ARPayment.docType, Equal<Current<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Current<ARPayment.refNbr>>>>>)})]
[PXGroupMask(typeof (InnerJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARPayment.customerID>, And<Match<Customer, Current<AccessInfo.userName>>>>>))]
public class ARPayment : 
  ARRegister,
  IInvoice,
  PX.Objects.CM.IRegister,
  IDocumentKey,
  ICCPayment,
  IApprovable,
  IAssign,
  IApprovalDescription,
  IReserved
{
  protected 
  #nullable disable
  string _PaymentMethodID;
  protected int? _PMInstanceID;
  protected int? _CashAccountID;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected bool? _UpdateNextNumber;
  protected string _ExtRefNbr;
  protected DateTime? _AdjDate;
  protected string _AdjFinPeriodID;
  protected string _AdjTranPeriodID;
  protected Decimal? _CuryConsolidateChargeTotal;
  protected Decimal? _ConsolidateChargeTotal;
  protected int? _ChargeCntr;
  protected Decimal? _CuryUnappliedBal;
  protected Decimal? _UnappliedBal;
  protected Decimal? _CuryApplAmt;
  protected Decimal? _CurySOApplAmt;
  protected Decimal? _SOApplAmt;
  protected Decimal? _ApplAmt;
  protected Decimal? _CuryWOAmt;
  protected Decimal? _WOAmt;
  protected bool? _Cleared;
  protected DateTime? _ClearDate;
  protected string _DrCr;
  protected long? _CATranID;
  protected bool? _IsCCPayment;
  protected string _CCPaymentStateDescr;
  protected bool? _DepositAsBatch;
  protected DateTime? _DepositAfter;
  protected DateTime? _DepositDate;
  protected bool? _Deposited;
  protected string _DepositType;
  protected string _DepositNbr;
  protected int? _CARefTranAccountID;
  protected long? _CARefTranID;
  protected int? _CARefSplitLineNbr;
  protected string _RefTranExtNbr;

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  [ARPaymentType.ListEx]
  [PXUIField]
  [PXFieldDescription]
  public override string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [ARPaymentType.RefNbr(typeof (Search2<ARRegisterAlias.refNbr, InnerJoinSingleTable<ARPayment, On<ARPayment.docType, Equal<ARRegisterAlias.docType>, And<ARPayment.refNbr, Equal<ARRegisterAlias.refNbr>>>, InnerJoinSingleTable<Customer, On<ARRegisterAlias.customerID, Equal<Customer.bAccountID>>>>, Where<ARRegisterAlias.docType, Equal<Current<ARPayment.docType>>, And<Match<Customer, Current<AccessInfo.userName>>>>, OrderBy<Desc<ARRegisterAlias.refNbr>>>), Filterable = true, IsPrimaryViewCompatible = true)]
  [ARPaymentType.Numbering]
  [PXFieldDescription]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [Customer]
  [PXRestrictor(typeof (Where<Customer.status, Equal<CustomerStatus.active>, Or<Customer.status, Equal<CustomerStatus.oneTime>, Or<Customer.status, Equal<CustomerStatus.hold>, Or<Customer.status, Equal<CustomerStatus.creditHold>>>>>), "The customer status is '{0}'.", new System.Type[] {typeof (Customer.status)})]
  [PXUIField(DisplayName = "Customer", TabOrder = 2)]
  [PXDefault]
  public override int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal]
  public virtual Decimal? CuryOrigTaxDiscAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal]
  public virtual Decimal? OrigTaxDiscAmt { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Coalesce<Search2<CustomerPaymentMethod.paymentMethodID, InnerJoin<Customer, On<CustomerPaymentMethod.bAccountID, Equal<Customer.bAccountID>>>, Where<Customer.bAccountID, Equal<Current<ARPayment.customerID>>, And<CustomerPaymentMethod.pMInstanceID, Equal<Customer.defPMInstanceID>>>>, Search<Customer.defPaymentMethodID, Where<Customer.bAccountID, Equal<Current<ARPayment.customerID>>>>>))]
  [PXSelector(typeof (Search5<PX.Objects.CA.PaymentMethod.paymentMethodID, LeftJoin<CustomerPaymentMethod, On<CustomerPaymentMethod.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>, And<CustomerPaymentMethod.bAccountID, Equal<Current<ARPayment.customerID>>>>, LeftJoin<CCProcessingCenterPmntMethod, On<CCProcessingCenterPmntMethod.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>>, LeftJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>>>, Where<PX.Objects.CA.PaymentMethod.isActive, Equal<True>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>>>, Aggregate<GroupBy<PX.Objects.CA.PaymentMethod.paymentMethodID, GroupBy<PX.Objects.CA.PaymentMethod.useForAR, GroupBy<PX.Objects.CA.PaymentMethod.useForAP>>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXUIField(DisplayName = "Payment Method", Enabled = false)]
  [PXForeignReference(typeof (Field<ARPayment.paymentMethodID>.IsRelatedTo<PX.Objects.CA.PaymentMethod.paymentMethodID>))]
  public virtual string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0)]
  [PXFormula(typeof (Switch<Case<Where<PendingValue<ARPayment.branchID>, IsPending>, Null, Case<Where<ARPayment.customerLocationID, IsNotNull, And<Selector<ARPayment.customerLocationID, PX.Objects.CR.Location.cBranchID>, IsNotNull>>, Selector<ARPayment.customerLocationID, PX.Objects.CR.Location.cBranchID>, Case<Where<Current2<ARPayment.branchID>, IsNotNull>, Current2<ARPayment.branchID>>>>, Current<AccessInfo.branchID>>))]
  public override int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  [PXDefault(typeof (Coalesce<Search2<Customer.defPMInstanceID, InnerJoin<CustomerPaymentMethod, On<CustomerPaymentMethod.pMInstanceID, Equal<Customer.defPMInstanceID>, And<CustomerPaymentMethod.bAccountID, Equal<Customer.bAccountID>>>>, Where<Customer.bAccountID, Equal<Current<ARPayment.customerID>>, And<CustomerPaymentMethod.isActive, Equal<True>, And<CustomerPaymentMethod.paymentMethodID, Equal<Current2<ARPayment.paymentMethodID>>>>>>, Search2<CustomerPaymentMethod.pMInstanceID, LeftJoin<CCProcessingCenterPmntMethodBranch, On<CustomerPaymentMethod.paymentMethodID, Equal<CCProcessingCenterPmntMethodBranch.paymentMethodID>, And<CustomerPaymentMethod.cCProcessingCenterID, Equal<CCProcessingCenterPmntMethodBranch.processingCenterID>, And<Current2<ARPayment.branchID>, Equal<CCProcessingCenterPmntMethodBranch.branchID>>>>>, Where<CustomerPaymentMethod.bAccountID, Equal<Current<ARPayment.customerID>>, And<CustomerPaymentMethod.paymentMethodID, Equal<Current2<ARPayment.paymentMethodID>>, And<CustomerPaymentMethod.isActive, Equal<True>>>>, OrderBy<Asc<Switch<Case<Where<CCProcessingCenterPmntMethodBranch.paymentMethodID, IsNull>, True>, False>, Desc<CustomerPaymentMethod.expirationDate, Desc<CustomerPaymentMethod.pMInstanceID>>>>>>))]
  [PXSelector(typeof (Search<CustomerPaymentMethod.pMInstanceID, Where<CustomerPaymentMethod.bAccountID, Equal<Current<ARPayment.customerID>>, And<CustomerPaymentMethod.paymentMethodID, Equal<Current2<ARPayment.paymentMethodID>>, And<Where<CustomerPaymentMethod.isActive, Equal<True>, Or<CustomerPaymentMethod.pMInstanceID, Equal<Current<ARPayment.pMInstanceID>>>>>>>>), DescriptionField = typeof (CustomerPaymentMethod.descr))]
  [DeprecatedProcessing]
  [DisabledProcCenter]
  [PXForeignReference(typeof (CompositeKey<Field<ARPayment.customerID>.IsRelatedTo<CustomerPaymentMethod.bAccountID>, Field<ARPayment.pMInstanceID>.IsRelatedTo<CustomerPaymentMethod.pMInstanceID>>))]
  [PXExcludeRowsFromReferentialIntegrityCheck(ForeignTableExcludingConditions = typeof (ExcludeWhen<PX.Objects.CA.PaymentMethod>.Joined<On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<ARPayment.paymentMethodID>>>.Satisfies<Where<PX.Objects.CA.PaymentMethod.paymentType, In3<PaymentMethodType.creditCard, PaymentMethodType.eft, PaymentMethodType.posTerminal>>>))]
  public virtual int? PMInstanceID
  {
    get => this._PMInstanceID;
    set => this._PMInstanceID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Proc. Center ID")]
  [PXDefault(typeof (Coalesce<Search<CustomerPaymentMethod.cCProcessingCenterID, Where<CustomerPaymentMethod.pMInstanceID, Equal<Current2<ARPayment.pMInstanceID>>>>, Coalesce<Search2<CCProcessingCenterPmntMethod.processingCenterID, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<CCProcessingCenterPmntMethod.paymentMethodID>>, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>, InnerJoin<CCProcessingCenterPmntMethodBranch, On<CCProcessingCenterPmntMethodBranch.paymentMethodID, Equal<CCProcessingCenterPmntMethod.paymentMethodID>, And<CCProcessingCenterPmntMethodBranch.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>>>>, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<ARPayment.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And2<Where<PX.Objects.CA.PaymentMethod.paymentType, NotEqual<PaymentMethodType.posTerminal>, And<CCProcessingCenterPmntMethodBranch.branchID, Equal<Current<ARPayment.branchID>>, Or<CCProcessingCenterPmntMethodBranch.branchID, Equal<Current<AccessInfo.branchID>>>>>, And<CCProcessingCenterPmntMethodBranch.paymentMethodID, Equal<Current<ARPayment.paymentMethodID>>, And<CCProcessingCenter.isActive, Equal<True>>>>>>>, Search2<CCProcessingCenterPmntMethod.processingCenterID, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<ARPayment.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<CCProcessingCenterPmntMethod.isDefault, Equal<True>, And<CCProcessingCenter.isActive, Equal<True>>>>>>>>))]
  [PXSelector(typeof (Search2<CCProcessingCenter.processingCenterID, InnerJoin<CCProcessingCenterPmntMethod, On<CCProcessingCenterPmntMethod.processingCenterID, Equal<CCProcessingCenter.processingCenterID>>>, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<ARPayment.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<CCProcessingCenter.isActive, Equal<True>>>>>), DescriptionField = typeof (CCProcessingCenter.name), ValidateValue = false)]
  [DeprecatedProcessing(ChckVal = DeprecatedProcessingAttribute.CheckVal.ProcessingCenterId)]
  [DisabledProcCenter(CheckFieldValue = DisabledProcCenterAttribute.CheckFieldVal.ProcessingCenterId)]
  public virtual string ProcessingCenterID { get; set; }

  [PXDBBool]
  public virtual bool? SyncLock { get; set; }

  [PXDBString(1, IsFixed = true, IsUnicode = false)]
  public virtual string SyncLockReason { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "New Card")]
  public virtual bool? NewCard { get; set; }

  /// <summary>Terminal ID</summary>
  [PXDBString(36, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Terminal")]
  [PXSelector(typeof (Search<CCProcessingCenterTerminal.terminalID, Where<CCProcessingCenterTerminal.processingCenterID, Equal<Current<ARPayment.processingCenterID>>, And<CCProcessingCenterTerminal.isActive, Equal<True>>>>), new System.Type[] {typeof (CCProcessingCenterTerminal.displayName)}, SubstituteKey = typeof (CCProcessingCenterTerminal.displayName))]
  [PXDefault(typeof (Search2<DefaultTerminal.terminalID, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<ARPayment.paymentMethodID>>>, InnerJoin<CCProcessingCenterTerminal, On<CCProcessingCenterTerminal.processingCenterID, Equal<DefaultTerminal.processingCenterID>, And<CCProcessingCenterTerminal.terminalID, Equal<DefaultTerminal.terminalID>>>>>, Where<PX.Objects.CA.PaymentMethod.paymentType, Equal<PaymentMethodType.posTerminal>, And<DefaultTerminal.userID, Equal<Current<AccessInfo.userID>>, And<DefaultTerminal.branchID, Equal<Current<AccessInfo.branchID>>, And<DefaultTerminal.processingCenterID, Equal<Current<ARPayment.processingCenterID>>, And<CCProcessingCenterTerminal.isActive, Equal<True>>>>>>>))]
  public virtual string TerminalID { get; set; }

  /// <summary>Indicates whether the payment is made in card-present mode</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? CardPresent { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "New Account")]
  public virtual bool? NewAccount
  {
    get => this.NewCard;
    set => this.NewCard = value;
  }

  [PXDefault(typeof (Coalesce<Search2<CustomerPaymentMethod.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<CustomerPaymentMethod.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<CustomerPaymentMethod.paymentMethodID>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>, Where<CustomerPaymentMethod.bAccountID, Equal<Current<ARPayment.customerID>>, And<Current<ARPayment.docType>, NotEqual<ARDocType.refund>, And<CustomerPaymentMethod.pMInstanceID, Equal<Current2<ARPayment.pMInstanceID>>>>>>, Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.useForAR, Equal<True>, And2<Where2<Where<Current<ARPayment.docType>, NotEqual<ARDocType.refund>, And<PaymentMethodAccount.aRIsDefault, Equal<True>>>, Or<Where<Current<ARPayment.docType>, Equal<ARDocType.refund>, And<PaymentMethodAccount.aRIsDefaultForRefund, Equal<True>>>>>, And<PaymentMethodAccount.paymentMethodID, Equal<Current2<ARPayment.paymentMethodID>>>>>>>, Where<PX.Objects.CA.CashAccount.branchID, Equal<Current<ARPayment.branchID>>, And<Match<Current<AccessInfo.userName>>>>>>))]
  [CashAccount(typeof (ARPayment.branchID), typeof (Search<PX.Objects.CA.CashAccount.cashAccountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.cashAccountID, In2<Search<PaymentMethodAccount.cashAccountID, Where<PaymentMethodAccount.paymentMethodID, Equal<Current2<ARPayment.paymentMethodID>>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>>>>))]
  public virtual int? CashAccountID
  {
    get => this._CashAccountID;
    set => this._CashAccountID = value;
  }

  [ProjectDefault("AR", typeof (Search<PX.Objects.CR.Location.cDefProjectID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<ARPayment.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<ARPayment.customerLocationID>>, And<ARDocType.creditMemo, NotEqual<Current<ARPayment.docType>>>>>>), typeof (ARPayment.cashAccountID))]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.visibleInAR, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<ARPayment.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ActiveProjectTask(typeof (ARPayment.projectID), "AR", NeedTaskValidationField = typeof (ARPayment.needTaskValidation), DisplayName = "Project Task")]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? UpdateNextNumber
  {
    get => this._UpdateNextNumber;
    set => this._UpdateNextNumber = value;
  }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField]
  [PXDefault]
  [PaymentRef(typeof (ARPayment.cashAccountID), typeof (ARPayment.paymentMethodID), typeof (ARPayment.updateNextNumber), typeof (ARPayment.isMigratedRecord))]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? AdjDate
  {
    get => this._AdjDate;
    set => this._AdjDate = value;
  }

  [AROpenPeriod(typeof (ARPayment.adjDate), typeof (ARPayment.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.All, null, typeof (ARPayment.adjTranPeriodID), IsHeader = true)]
  [PXUIField]
  public virtual string AdjFinPeriodID
  {
    get => this._AdjFinPeriodID;
    set => this._AdjFinPeriodID = value;
  }

  [PeriodID(null, null, null, true)]
  public virtual string AdjTranPeriodID
  {
    get => this._AdjTranPeriodID;
    set => this._AdjTranPeriodID = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public override DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  [PeriodID(null, null, null, true)]
  public override string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  [AROpenPeriod(typeof (ARPayment.docDate), null, null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.All, new System.Type[] {typeof (PeriodKeyProviderBase.SourceSpecification<ARPayment.branchID, True>), typeof (PeriodKeyProviderBase.SourceSpecification<ARPayment.cashAccountID, Selector<ARPayment.cashAccountID, PX.Objects.CA.CashAccount.branchID>, False>)}, typeof (ARPayment.tranPeriodID))]
  [PXDefault]
  [PXUIField]
  public override string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARPayment.curyInfoID), typeof (ARPayment.origDocAmt))]
  [PXUIField]
  public override Decimal? CuryOrigDocAmt
  {
    get => this._CuryOrigDocAmt;
    set => this._CuryOrigDocAmt = value;
  }

  [PXDBCurrency(typeof (ARPayment.curyInfoID), typeof (ARPayment.consolidateChargeTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryConsolidateChargeTotal
  {
    get => this._CuryConsolidateChargeTotal;
    set => this._CuryConsolidateChargeTotal = value;
  }

  [PXDBDecimal(4)]
  public virtual Decimal? ConsolidateChargeTotal
  {
    get => this._ConsolidateChargeTotal;
    set => this._ConsolidateChargeTotal = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ChargeCntr
  {
    get => this._ChargeCntr;
    set => this._ChargeCntr = value;
  }

  [PXCurrency(typeof (ARPayment.curyInfoID), typeof (ARPayment.unappliedBal))]
  [PXUIField]
  [PXFormula(typeof (Sub<ARPayment.curyDocBal, Add<ARPayment.curyApplAmt, ARPayment.curySOApplAmt>>))]
  public virtual Decimal? CuryUnappliedBal
  {
    get => this._CuryUnappliedBal;
    set => this._CuryUnappliedBal = value;
  }

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
  [PXDBCurrency(typeof (ARRegister.curyInfoID), typeof (ARRegister.initDocBal))]
  [PXUIField]
  [PXUIVerify]
  public override Decimal? CuryInitDocBal { get; set; }

  [PXCurrency(typeof (ARPayment.curyInfoID), typeof (ARPayment.applAmt))]
  [PXUIField(DisplayName = "Applied to Documents", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDependsOnFields(new System.Type[] {typeof (ARPayment.adjCntr), typeof (ARPayment.released)})]
  public virtual Decimal? CuryApplAmt
  {
    get => this._CuryApplAmt;
    set => this._CuryApplAmt = value;
  }

  [PXCurrency(typeof (ARPayment.curyInfoID), typeof (ARPayment.sOApplAmt))]
  [PXUIField(DisplayName = "Applied to Orders", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CurySOApplAmt
  {
    get => this._CurySOApplAmt;
    set => this._CurySOApplAmt = value;
  }

  [PXDecimal(4)]
  public virtual Decimal? SOApplAmt
  {
    get => this._SOApplAmt;
    set => this._SOApplAmt = value;
  }

  [PXDecimal(4)]
  public virtual Decimal? ApplAmt
  {
    get => this._ApplAmt;
    set => this._ApplAmt = value;
  }

  [PXCurrency(typeof (ARPayment.curyInfoID), typeof (ARPayment.wOAmt))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryWOAmt
  {
    get => this._CuryWOAmt;
    set => this._CuryWOAmt = value;
  }

  [PXDecimal(4)]
  public virtual Decimal? WOAmt
  {
    get => this._WOAmt;
    set => this._WOAmt = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cleared")]
  public virtual bool? Cleared
  {
    get => this._Cleared;
    set => this._Cleared = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? ClearDate
  {
    get => this._ClearDate;
    set => this._ClearDate = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public override bool? Voided
  {
    get => this._Voided;
    set => this._Voided = value;
  }

  [PXBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? VoidAppl
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARPayment.docType)})] get
    {
      return new bool?(ARPaymentType.VoidAppl(this._DocType));
    }
    set
    {
      if (!value.Value || ARPaymentType.VoidAppl(this.DocType))
        return;
      this.DocType = ARPaymentType.GetVoidingARDocType(this.DocType);
    }
  }

  [PXBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? CanHaveBalance
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARPayment.docType)})] get
    {
      return new bool?(ARPaymentType.CanHaveBalance(this._DocType));
    }
    set
    {
    }
  }

  [PXString(1, IsFixed = true)]
  public virtual string DrCr
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARPayment.docType)})] get
    {
      return ARPaymentType.DrCr(this._DocType);
    }
    set
    {
    }
  }

  [PXDBLong]
  [ARCashTranID]
  public virtual long? CATranID
  {
    get => this._CATranID;
    set => this._CATranID = value;
  }

  public virtual DateTime? DiscDate { get; set; }

  public virtual Decimal? CuryWhTaxBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  public virtual Decimal? WhTaxBal
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXFormula(typeof (Switch<Case<Where<ARPayment.isMigratedRecord, Equal<False>, And<Selector<ARPayment.paymentMethodID, PX.Objects.CA.PaymentMethod.paymentType>, In3<PaymentMethodType.creditCard, PaymentMethodType.eft, PaymentMethodType.posTerminal>, And<Selector<ARPayment.paymentMethodID, PX.Objects.CA.PaymentMethod.aRIsProcessingRequired>, Equal<True>>>>, True>, False>))]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual bool? IsCCPayment
  {
    get => this._IsCCPayment;
    set => this._IsCCPayment = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCCAuthorized { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCCCaptured { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCCCaptureFailed { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCCRefunded { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsCCUserAttention { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Save Card")]
  public virtual bool? SaveCard { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Save Account")]
  public virtual bool? SaveAccount
  {
    get => this.SaveCard;
    set => this.SaveCard = value;
  }

  [PXString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Processing Status", Enabled = false)]
  public virtual string CCPaymentStateDescr
  {
    get => this._CCPaymentStateDescr;
    set => this._CCPaymentStateDescr = value;
  }

  [PXDBBool]
  [PXDefault(false, typeof (Search<PX.Objects.CA.CashAccount.clearingAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<ARPayment.cashAccountID>>>>))]
  [PXUIField(DisplayName = "Batch Deposit", Enabled = false)]
  public virtual bool? DepositAsBatch
  {
    get => this._DepositAsBatch;
    set => this._DepositAsBatch = value;
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Deposit After", Enabled = false, Visible = false)]
  public virtual DateTime? DepositAfter
  {
    get => this._DepositAfter;
    set => this._DepositAfter = value;
  }

  [PXDate]
  [PXUIField(DisplayName = "Batch Deposit Date", Enabled = false)]
  public virtual DateTime? DepositDate
  {
    get => this._DepositDate;
    set => this._DepositDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Deposited", Enabled = false)]
  public virtual bool? Deposited
  {
    get => this._Deposited;
    set => this._Deposited = value;
  }

  [PXDBString(3, IsFixed = true)]
  public virtual string DepositType
  {
    get => this._DepositType;
    set => this._DepositType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Deposit Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<CADeposit.refNbr, Where<CADeposit.tranType, Equal<Current<ARPayment.depositType>>>>))]
  public virtual string DepositNbr
  {
    get => this._DepositNbr;
    set => this._DepositNbr = value;
  }

  public virtual int? CARefTranAccountID
  {
    get => this._CARefTranAccountID;
    set => this._CARefTranAccountID = value;
  }

  public virtual long? CARefTranID
  {
    get => this._CARefTranID;
    set => this._CARefTranID = value;
  }

  public virtual int? CARefSplitLineNbr
  {
    get => this._CARefSplitLineNbr;
    set => this._CARefSplitLineNbr = value;
  }

  [PXDBBool]
  [PXDefault(typeof (IIf<Where<Current<ARPayment.docType>, Equal<ARDocType.refund>, And<Selector<ARPayment.paymentMethodID, PX.Objects.CA.PaymentMethod.paymentType>, In3<PaymentMethodType.creditCard, PaymentMethodType.eft, PaymentMethodType.posTerminal>, And<Selector<ARPayment.paymentMethodID, PX.Objects.CA.PaymentMethod.aRIsProcessingRequired>, Equal<True>>>>, True, False>))]
  [PXUIField(DisplayName = "Use Orig. Transaction for Refund", Visible = false)]
  public virtual bool? CCTransactionRefund { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXDefault]
  [PX.Objects.AR.RefTranExtNbr]
  [PXUIField]
  public virtual string RefTranExtNbr
  {
    get => this._RefTranExtNbr;
    set => this._RefTranExtNbr = value;
  }

  [PXDBDateAndTime]
  public virtual DateTime? CCReauthDate { get; set; }

  [PXDBInt]
  public virtual int? CCReauthTriesLeft { get; set; }

  [PXDBInt]
  [PXDBChildIdentity(typeof (ExternalTransaction.transactionID))]
  public virtual int? CCActualExternalTransactionID { get; set; }

  [PXSearchable(2, "AR {0}: {1} - {3}", new System.Type[] {typeof (ARPayment.docType), typeof (ARPayment.refNbr), typeof (ARPayment.customerID), typeof (Customer.acctName)}, new System.Type[] {typeof (ARPayment.extRefNbr), typeof (ARRegister.docDesc)}, NumberFields = new System.Type[] {typeof (ARPayment.refNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (ARPayment.docDate), typeof (ARPayment.status), typeof (ARPayment.extRefNbr)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (ARRegister.docDesc)}, MatchWithJoin = typeof (InnerJoin<Customer, On<Customer.bAccountID, Equal<ARPayment.customerID>>>), SelectForFastIndexing = typeof (Select2<ARPayment, InnerJoin<Customer, On<ARPayment.customerID, Equal<Customer.bAccountID>>>>))]
  [PXNote]
  public override Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>
  /// Indicates whether validation for the presence of the correct <see cref="P:PX.Objects.AR.ARPayment.TaskID" /> must be performed for the line before it is persisted to the database.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  public virtual bool? NeedTaskValidation
  {
    [PXDependsOnFields(new System.Type[] {typeof (ARPayment.docType)})] get
    {
      return this.DocType == "CRM" ? new bool?(false) : new bool?(true);
    }
    set
    {
    }
  }

  [PXBool]
  public virtual bool? PostponeReleasedFlag { get; set; }

  [PXBool]
  public virtual bool? PostponeVoidedFlag { get; set; }

  /// <summary>
  /// Indicates that events will be invoke in the ARDocumentRelease.ProcessPostponedFlags() method
  /// for <see cref="T:PX.Objects.AR.ARPayment.pendingPayment" /> field
  /// </summary>
  [PXBool]
  public override bool? PostponePendingPaymentFlag
  {
    get => base.PostponePendingPaymentFlag;
    set => base.PostponePendingPaymentFlag = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Settled")]
  public virtual bool? Settled { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that the document should not be sent to the <see cref="P:PX.Objects.AR.ARPayment.CustomerID">Customer</see>
  /// as a printed document, and thus the system should not include it in the list of documents available for mass-printing.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Don't Print", Enabled = false)]
  public override bool? DontPrint { get; set; }

  /// <summary>
  /// When set to <c>true</c> indicates that the document should not be sent to the <see cref="P:PX.Objects.AR.ARPayment.CustomerID">Customer</see>
  /// by email, and thus the system should not include it in the list of documents available for mass-emailing.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (IIf<Where<Selector<ARPayment.paymentMethodID, PX.Objects.CA.PaymentMethod.sendPaymentReceiptsAutomatically>, Equal<True>>, False, True>))]
  [PXUIField(DisplayName = "Don't Email")]
  public override bool? DontEmail { get; set; }

  string ICCPayment.OrigDocType => (string) null;

  string ICCPayment.OrigRefNbr => (string) null;

  Decimal? ICCPayment.CuryDocBal
  {
    get => this.CuryOrigDocAmt;
    set
    {
    }
  }

  [PXBool]
  public virtual bool? OrigReleased { get; set; }

  /// <exclude />
  public new class PK : PrimaryKeyOf<ARPayment>.By<ARPayment.docType, ARPayment.refNbr>
  {
    public static ARPayment Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (ARPayment) PrimaryKeyOf<ARPayment>.By<ARPayment.docType, ARPayment.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARPayment>.By<ARPayment.branchID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARPayment>.By<ARPayment.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<ARPayment>.By<ARPayment.customerID, ARPayment.customerLocationID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARPayment>.By<ARPayment.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<ARPayment>.By<ARPayment.curyID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<PX.Objects.CA.CashAccount>.By<PX.Objects.CA.CashAccount.cashAccountID>.ForeignKeyOf<ARPayment>.By<ARPayment.cashAccountID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<ARPayment>.By<ARPayment.paymentMethodID>
    {
    }

    public class CustomerPaymentMethod : 
      PrimaryKeyOf<CustomerPaymentMethod>.By<CustomerPaymentMethod.pMInstanceID>.ForeignKeyOf<ARPayment>.By<ARPayment.pMInstanceID>
    {
    }
  }

  public new class Events : PXEntityEventBase<ARPayment>.Container<ARPayment.Events>
  {
    public PXEntityEvent<ARPayment> ReleaseDocument;
    public PXEntityEvent<ARPayment> CloseDocument;
    public PXEntityEvent<ARPayment> OpenDocument;
    public PXEntityEvent<ARPayment> VoidDocument;
  }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.selected>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.docType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.refNbr>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.customerID>
  {
  }

  public new abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARPayment.customerLocationID>
  {
  }

  public abstract class curyOrigTaxDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPayment.curyOrigTaxDiscAmt>
  {
  }

  public abstract class origTaxDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPayment.origTaxDiscAmt>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.status>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPayment.paymentMethodID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.branchID>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.pMInstanceID>
  {
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPayment.processingCenterID>
  {
  }

  public abstract class syncLock : IBqlField, IBqlOperand
  {
  }

  public abstract class syncLockReason : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.syncLockReason>
  {
    public const string NewCard = "N";
    public const string NeedValidation = "V";

    public class newCard : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARPayment.syncLockReason.newCard>
    {
      public newCard()
        : base("N")
      {
      }
    }

    public class needValidation : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ARPayment.syncLockReason.newCard>
    {
      public needValidation()
        : base("V")
      {
      }
    }
  }

  public abstract class newCard : IBqlField, IBqlOperand
  {
  }

  public abstract class terminalID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.terminalID>
  {
  }

  public abstract class cardPresent : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.cardPresent>
  {
  }

  public abstract class newAccount : IBqlField, IBqlOperand
  {
  }

  public abstract class pMInstanceID_CustomerPaymentMethod_descr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPayment.pMInstanceID_CustomerPaymentMethod_descr>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.cashAccountID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.taskID>
  {
  }

  public abstract class updateNextNumber : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.updateNextNumber>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.extRefNbr>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.curyID>
  {
  }

  public abstract class adjDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARPayment.adjDate>
  {
  }

  public abstract class adjFinPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.adjFinPeriodID>
  {
  }

  public abstract class adjTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPayment.adjTranPeriodID>
  {
  }

  public new abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARPayment.docDate>
  {
  }

  public new abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.tranPeriodID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.finPeriodID>
  {
  }

  public new abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARPayment.curyDocBal>
  {
  }

  public new abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPayment.curyOrigDocAmt>
  {
  }

  public new abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARPayment.origDocAmt>
  {
  }

  public abstract class curyConsolidateChargeTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPayment.curyConsolidateChargeTotal>
  {
  }

  public abstract class consolidateChargeTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPayment.consolidateChargeTotal>
  {
  }

  public new abstract class adjCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.adjCntr>
  {
  }

  public abstract class chargeCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.chargeCntr>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARPayment.curyInfoID>
  {
  }

  public abstract class curyUnappliedBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPayment.curyUnappliedBal>
  {
  }

  public abstract class unappliedBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARPayment.unappliedBal>
  {
  }

  public new abstract class curyInitDocBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPayment.curyInitDocBal>
  {
  }

  public abstract class curyApplAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARPayment.curyApplAmt>
  {
  }

  public abstract class curySOApplAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARPayment.curySOApplAmt>
  {
  }

  public abstract class sOApplAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARPayment.sOApplAmt>
  {
  }

  public abstract class applAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARPayment.applAmt>
  {
  }

  public abstract class curyWOAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARPayment.curyWOAmt>
  {
  }

  public abstract class wOAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARPayment.wOAmt>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARPayment.clearDate>
  {
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.batchNbr>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.voided>
  {
  }

  public abstract class voidAppl : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.voidAppl>
  {
  }

  public new abstract class customerID_Customer_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPayment.customerID_Customer_acctName>
  {
  }

  public abstract class canHaveBalance : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.canHaveBalance>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.released>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.hold>
  {
  }

  public new abstract class openDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.openDoc>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.drCr>
  {
  }

  public abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARPayment.cATranID>
  {
  }

  public abstract class CustomerPaymentMethod_descr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPayment.CustomerPaymentMethod_descr>
  {
  }

  public abstract class isCCPayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.isCCPayment>
  {
  }

  public abstract class isCCAuthorized : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.isCCAuthorized>
  {
  }

  public abstract class isCCCaptured : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.isCCCaptured>
  {
  }

  public abstract class isCCCaptureFailed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPayment.isCCCaptureFailed>
  {
  }

  public abstract class isCCRefunded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.isCCRefunded>
  {
  }

  public abstract class isCCUserAttention : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPayment.isCCUserAttention>
  {
  }

  public new abstract class pendingProcessing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPayment.pendingProcessing>
  {
  }

  public abstract class saveCard : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.saveCard>
  {
  }

  public abstract class saveAccount : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.saveAccount>
  {
  }

  public abstract class cCPaymentStateDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPayment.cCPaymentStateDescr>
  {
  }

  public abstract class depositAsBatch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.depositAsBatch>
  {
  }

  public abstract class depositAfter : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARPayment.depositAfter>
  {
  }

  public abstract class depositDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARPayment.depositDate>
  {
  }

  public abstract class deposited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.deposited>
  {
  }

  public abstract class depositType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.depositType>
  {
  }

  public abstract class depositNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.depositNbr>
  {
  }

  public abstract class cCTransactionRefund : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPayment.cCTransactionRefund>
  {
  }

  public abstract class refTranExtNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.refTranExtNbr>
  {
  }

  public abstract class cCReauthDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARPayment.cCReauthDate>
  {
  }

  public abstract class cCReauthTriesLeft : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARPayment.cCReauthTriesLeft>
  {
  }

  public abstract class cCActualExternalTransactionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARPayment.cCActualExternalTransactionID>
  {
  }

  public new abstract class isMigratedRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPayment.isMigratedRecord>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARPayment.noteID>
  {
  }

  public abstract class needTaskValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPayment.needTaskValidation>
  {
  }

  public abstract class postponeReleasedFlag : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPayment.postponeReleasedFlag>
  {
  }

  public abstract class postponeVoidedFlag : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPayment.postponeVoidedFlag>
  {
  }

  public new abstract class postponePendingPaymentFlag : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPayment.postponePendingPaymentFlag>
  {
  }

  public abstract class settled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.settled>
  {
  }

  public new abstract class dontPrint : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.dontPrint>
  {
  }

  public new abstract class dontEmail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.dontEmail>
  {
  }

  public abstract class origReleased : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.origReleased>
  {
  }

  public new abstract class pendingPayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.pendingPayment>
  {
  }
}
