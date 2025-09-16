// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOQuickPayment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CC;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("SO Quick Payment")]
[PXVirtual]
public class SOQuickPayment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(typeof (Where<Current<SOOrderType.canHaveRefunds>, Equal<True>, And<Where<Current<SOOrder.behavior>, NotEqual<SOBehavior.mO>, Or<Current<SOOrder.curyOrderTotal>, Less<decimal0>>>>>))]
  public virtual bool? IsRefund { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.isActive, Equal<boolTrue>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<boolTrue>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  [PXUIField(DisplayName = "Payment Method", Required = true)]
  public virtual 
  #nullable disable
  string PaymentMethodID { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXDefault(typeof (Search2<CCProcessingCenterPmntMethod.processingCenterID, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<SOQuickPayment.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<CCProcessingCenterPmntMethod.isDefault, Equal<True>, And<CCProcessingCenter.isActive, Equal<True>>>>>>))]
  [PXSelector(typeof (Search<CCProcessingCenter.processingCenterID>), ValidateValue = false)]
  public virtual string PaymentMethodProcCenterID { get; set; }

  /// <summary>Terminal ID</summary>
  [PXString(36, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<CCProcessingCenterTerminal.terminalID, Where<CCProcessingCenterTerminal.processingCenterID, Equal<Current<SOQuickPayment.processingCenterID>>, And<CCProcessingCenterTerminal.isActive, Equal<True>>>>), new Type[] {typeof (CCProcessingCenterTerminal.displayName)}, SubstituteKey = typeof (CCProcessingCenterTerminal.displayName))]
  [PXDefault(typeof (Search2<DefaultTerminal.terminalID, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<SOQuickPayment.paymentMethodID>>>, InnerJoin<CCProcessingCenterTerminal, On<CCProcessingCenterTerminal.processingCenterID, Equal<DefaultTerminal.processingCenterID>, And<CCProcessingCenterTerminal.terminalID, Equal<DefaultTerminal.terminalID>>>>>, Where<PX.Objects.CA.PaymentMethod.paymentType, Equal<PaymentMethodType.posTerminal>, And<DefaultTerminal.userID, Equal<Current<AccessInfo.userID>>, And<DefaultTerminal.branchID, Equal<Current<AccessInfo.branchID>>, And<DefaultTerminal.processingCenterID, Equal<Current<SOQuickPayment.processingCenterID>>, And<CCProcessingCenterTerminal.isActive, Equal<True>>>>>>>))]
  public virtual string TerminalID { get; set; }

  [PXBool]
  [PXFormula(typeof (IsNull<Selector<SOQuickPayment.paymentMethodProcCenterID, CCProcessingCenter.allowUnlinkedRefund>, True>))]
  public virtual bool? AllowUnlinkedRefund { get; set; }

  [PXString(50, IsUnicode = true)]
  [PXSelector(typeof (Search2<PX.Objects.AR.ExternalTransaction.tranNumber, InnerJoin<PX.Objects.AR.ARPayment, On<PX.Objects.AR.ExternalTransaction.docType, Equal<PX.Objects.AR.ARPayment.docType>, And<PX.Objects.AR.ExternalTransaction.refNbr, Equal<PX.Objects.AR.ARPayment.refNbr>>>>, Where<PX.Objects.AR.ExternalTransaction.procStatus, Equal<ExtTransactionProcStatusCode.captureSuccess>, And<PX.Objects.AR.ARPayment.customerID, Equal<Current2<SOOrder.customerID>>, And<PX.Objects.AR.ARPayment.paymentMethodID, Equal<Current2<SOQuickPayment.paymentMethodID>>>>>, OrderBy<Desc<PX.Objects.AR.ExternalTransaction.tranNumber>>>), new Type[] {typeof (PX.Objects.AR.ExternalTransaction.refNbr), typeof (PX.Objects.AR.ARPayment.docDate), typeof (PX.Objects.AR.ExternalTransaction.amount), typeof (PX.Objects.AR.ExternalTransaction.tranNumber)})]
  [PXUIField]
  public virtual string RefTranExtNbr { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.Customer.defPMInstanceID, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<PX.Objects.AR.Customer.defPMInstanceID>, And<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current2<SOQuickPayment.paymentMethodID>>>>>>, Search2<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, LeftJoin<CCProcessingCenterPmntMethodBranch, On<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<CCProcessingCenterPmntMethodBranch.paymentMethodID>, And<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<CCProcessingCenterPmntMethodBranch.processingCenterID>, And<Current2<SOOrder.branchID>, Equal<CCProcessingCenterPmntMethodBranch.branchID>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current2<SOQuickPayment.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>>>>, OrderBy<Asc<Switch<Case<Where<CCProcessingCenterPmntMethodBranch.paymentMethodID, IsNull>, True>, False>, Desc<PX.Objects.AR.CustomerPaymentMethod.expirationDate, Desc<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>>>))]
  [PXSelector(typeof (Search<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current2<SOOrder.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current2<SOQuickPayment.paymentMethodID>>, And<Where<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<boolTrue>, Or<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Current<SOQuickPayment.pMInstanceID>>>>>>>>), DescriptionField = typeof (PX.Objects.AR.CustomerPaymentMethod.descr))]
  [DeprecatedProcessing]
  [DisabledProcCenter]
  public virtual int? PMInstanceID { get; set; }

  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.CustomerPaymentMethod.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.AR.CustomerPaymentMethod.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Current2<SOQuickPayment.pMInstanceID>>>>>, Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.aRIsDefault, Equal<True>, And<PaymentMethodAccount.paymentMethodID, Equal<Current2<SOQuickPayment.paymentMethodID>>>>>>>, Where<PX.Objects.CA.CashAccount.branchID, Equal<Current<SOOrder.branchID>>, And<Match<Current<AccessInfo.userName>>>>>>))]
  [CashAccount(typeof (SOOrder.branchID), typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.paymentMethodID, Equal<Current2<SOQuickPayment.paymentMethodID>>>>>>, Where<Match<Current<AccessInfo.userName>>>>), SuppressCurrencyValidation = false, Required = true)]
  public virtual int? CashAccountID { get; set; }

  [PXString(40, IsUnicode = true)]
  [PXUIField]
  [PXDefault]
  [PaymentRef(typeof (SOQuickPayment.cashAccountID), typeof (SOQuickPayment.paymentMethodID), typeof (SOQuickPayment.updateNextNumber), typeof (SOQuickPayment.isMigratedRecordStub))]
  public virtual string ExtRefNbr { get; set; }

  [PXBool]
  [PXUIField]
  [PXDefault(true)]
  public virtual bool? UpdateNextNumber { get; set; }

  [PXString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string DocDesc { get; set; }

  [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Coalesce<Search<PX.Objects.CA.CashAccount.curyID, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<SOQuickPayment.cashAccountID>>>>, Search<PX.Objects.GL.Branch.baseCuryID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID { get; set; }

  [PXLong]
  [CurrencyInfo(ModuleCode = "AR")]
  public virtual long? CuryInfoID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (SOQuickPayment.curyInfoID), typeof (SOQuickPayment.origDocAmt))]
  [PXUIField]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigDocAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (SOQuickPayment.curyInfoID), typeof (SOQuickPayment.refundAmt))]
  [PXUIField]
  public virtual Decimal? CuryRefundAmt { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RefundAmt { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? IsMigratedRecordStub { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "New Card")]
  public virtual bool? NewCard { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "New Account")]
  public virtual bool? NewAccount
  {
    get => this.NewCard;
    set => this.NewCard = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Save Card")]
  public virtual bool? SaveCard { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Save Account")]
  public virtual bool? SaveAccount
  {
    get => this.SaveCard;
    set => this.SaveCard = value;
  }

  [PXString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Proc. Center ID")]
  [PXDefault(typeof (Coalesce<Search<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Where<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Current2<SOQuickPayment.pMInstanceID>>>>, Coalesce<Search2<CCProcessingCenterPmntMethod.processingCenterID, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<CCProcessingCenterPmntMethod.paymentMethodID>>, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>, InnerJoin<CCProcessingCenterPmntMethodBranch, On<CCProcessingCenterPmntMethodBranch.paymentMethodID, Equal<CCProcessingCenterPmntMethod.paymentMethodID>, And<CCProcessingCenterPmntMethodBranch.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>>>>, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<SOQuickPayment.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And2<Where<PX.Objects.CA.PaymentMethod.paymentType, NotEqual<PaymentMethodType.posTerminal>, And<CCProcessingCenterPmntMethodBranch.branchID, Equal<Current<SOOrder.branchID>>, Or<CCProcessingCenterPmntMethodBranch.branchID, Equal<Current<AccessInfo.branchID>>>>>, And<CCProcessingCenterPmntMethodBranch.paymentMethodID, Equal<Current<SOQuickPayment.paymentMethodID>>, And<CCProcessingCenter.isActive, Equal<True>>>>>>>, Search2<CCProcessingCenterPmntMethod.processingCenterID, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.processingCenterID, Equal<CCProcessingCenterPmntMethod.processingCenterID>>>, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<SOQuickPayment.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<CCProcessingCenterPmntMethod.isDefault, Equal<True>, And<CCProcessingCenter.isActive, Equal<True>>>>>>>>))]
  [PXSelector(typeof (Search2<CCProcessingCenter.processingCenterID, InnerJoin<CCProcessingCenterPmntMethod, On<CCProcessingCenterPmntMethod.processingCenterID, Equal<CCProcessingCenter.processingCenterID>>>, Where<CCProcessingCenterPmntMethod.paymentMethodID, Equal<Current<SOQuickPayment.paymentMethodID>>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<CCProcessingCenter.isActive, Equal<True>>>>>), DescriptionField = typeof (CCProcessingCenter.name), ValidateValue = false)]
  [DisabledProcCenter(CheckFieldValue = DisabledProcCenterAttribute.CheckFieldVal.ProcessingCenterId)]
  public virtual string ProcessingCenterID { get; set; }

  [PXBool]
  public virtual bool? Authorize { get; set; }

  [PXBool]
  public virtual bool? Capture { get; set; }

  /// <summary>Authorize remainder action for payment</summary>
  [PXBool]
  public virtual bool? AuthorizeRemainder { get; set; }

  [PXBool]
  public virtual bool? Refund { get; set; }

  [PXString(3, IsKey = true, IsFixed = true)]
  public virtual string AdjgDocType { get; set; }

  [PXString(15, IsUnicode = true, IsKey = true)]
  public virtual string AdjgRefNbr { get; set; }

  /// <summary>Previous external transaction id</summary>
  [PXString(50, IsUnicode = true)]
  public virtual string PreviousExternalTransactionID { get; set; }

  public abstract class isRefund : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOQuickPayment.isRefund>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOQuickPayment.paymentMethodID>
  {
  }

  public abstract class paymentMethodProcCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOQuickPayment.paymentMethodProcCenterID>
  {
  }

  public abstract class terminalID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOQuickPayment.terminalID>
  {
  }

  public abstract class allowUnlinkedRefund : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickPayment.allowUnlinkedRefund>
  {
  }

  public abstract class refTranExtNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOQuickPayment.refTranExtNbr>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOQuickPayment.pMInstanceID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOQuickPayment.cashAccountID>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOQuickPayment.extRefNbr>
  {
  }

  public abstract class updateNextNumber : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickPayment.updateNextNumber>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOQuickPayment.docDesc>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOQuickPayment.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOQuickPayment.curyInfoID>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOQuickPayment.curyOrigDocAmt>
  {
  }

  public abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOQuickPayment.origDocAmt>
  {
  }

  public abstract class curyRefundAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOQuickPayment.curyRefundAmt>
  {
  }

  public abstract class refundAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOQuickPayment.refundAmt>
  {
  }

  public abstract class isMigratedRecordStub : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickPayment.isMigratedRecordStub>
  {
  }

  public abstract class newCard : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOQuickPayment.newCard>
  {
  }

  public abstract class newAccount : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOQuickPayment.newAccount>
  {
  }

  public abstract class saveCard : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOQuickPayment.saveCard>
  {
  }

  public abstract class saveAccount : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOQuickPayment.saveAccount>
  {
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOQuickPayment.processingCenterID>
  {
  }

  public abstract class authorize : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOQuickPayment.authorize>
  {
  }

  public abstract class capture : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOQuickPayment.capture>
  {
  }

  public abstract class authorizeRemainder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickPayment.authorizeRemainder>
  {
  }

  public abstract class refund : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOQuickPayment.refund>
  {
  }

  public abstract class adjgDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOQuickPayment.adjgDocType>
  {
  }

  public abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOQuickPayment.adjgRefNbr>
  {
  }

  public abstract class previousExternalTransactionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOQuickPayment.previousExternalTransactionID>
  {
  }
}
