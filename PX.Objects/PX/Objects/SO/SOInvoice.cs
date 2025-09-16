// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOInvoice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.RelatedItems;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.SO;

[CRCacheIndependentPrimaryGraphList(new System.Type[] {typeof (SOInvoiceEntry)}, new System.Type[] {typeof (Select<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Current<SOInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Current<SOInvoice.refNbr>>>>>)})]
[PXCacheName("SO Invoice")]
[PXProjection(typeof (Select2<SOInvoice, InnerJoin<SOInvoice.ARRegister, On<SOInvoice.ARRegister.docType, Equal<SOInvoice.docType>, And<SOInvoice.ARRegister.refNbr, Equal<SOInvoice.refNbr>>>, LeftJoin<PX.Objects.AR.Standalone.ARPayment, On<SOInvoice.docType, Equal<PX.Objects.AR.Standalone.ARPayment.docType>, And<SOInvoice.refNbr, Equal<PX.Objects.AR.Standalone.ARPayment.refNbr>>>>>>), Persistent = true)]
[Serializable]
public class SOInvoice : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISubstitutableDocument
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected int? _CustomerID;
  protected long? _CuryInfoID;
  protected int? _BillAddressID;
  protected int? _BillContactID;
  protected int? _ShipAddressID;
  protected int? _ShipContactID;
  protected Decimal? _CuryManDisc;
  protected Decimal? _ManDisc;
  protected byte[] _tstamp;
  protected Decimal? _CuryPaymentAmt;
  protected Decimal? _PaymentAmt;
  protected string _CuryID;
  protected string _PaymentMethodID;
  protected int? _PMInstanceID;
  protected int? _CashAccountID;
  protected string _ExtRefNbr;
  protected bool? _Cleared;
  protected DateTime? _ClearDate;
  protected long? _CATranID;
  protected bool? _Released;
  protected bool? _Hold;
  protected string _DocDesc;
  protected DateTime? _DocDate;
  protected string _Status;
  protected bool? _IsTaxValid;
  protected Guid? _NoteID;
  protected DateTime? _AdjDate;
  protected string _AdjFinPeriodID;
  protected string _AdjTranPeriodID;
  protected bool? _DepositAsBatch;
  protected DateTime? _DepositAfter;
  protected bool? _Deposited;
  protected string _DepositType;
  protected string _DepositNbr;
  protected int? _ChargeCntr;
  protected Decimal? _CuryConsolidateChargeTotal;
  protected Decimal? _ConsolidateChargeTotal;
  protected int? _PaymentProjectID;
  protected int? _PaymentTaskID;
  protected string _SOOrderType;
  protected string _SOOrderNbr;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (PX.Objects.AR.ARInvoice.docType))]
  [ARDocType.List]
  [PXUIField(DisplayName = "Type")]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (PX.Objects.AR.ARInvoice.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.")]
  [PXParent(typeof (SOInvoice.FK.ARInvoice))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [CustomerActive]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (PX.Objects.AR.ARInvoice.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBInt]
  public virtual int? BillAddressID
  {
    get => this._BillAddressID;
    set => this._BillAddressID = value;
  }

  [PXDBInt]
  public virtual int? BillContactID
  {
    get => this._BillContactID;
    set => this._BillContactID = value;
  }

  [PXDBInt]
  public virtual int? ShipAddressID
  {
    get => this._ShipAddressID;
    set => this._ShipAddressID = value;
  }

  [PXDBInt]
  public virtual int? ShipContactID
  {
    get => this._ShipContactID;
    set => this._ShipContactID = value;
  }

  [PXDBCurrency(typeof (SOInvoice.curyInfoID), typeof (SOInvoice.manDisc))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Manual Total")]
  public virtual Decimal? CuryManDisc
  {
    get => this._CuryManDisc;
    set => this._CuryManDisc = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ManDisc
  {
    get => this._ManDisc;
    set => this._ManDisc = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCurrency(typeof (SOInvoice.curyInfoID), typeof (SOInvoice.paymentAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Payment Amount", Enabled = false)]
  public virtual Decimal? CuryPaymentAmt
  {
    get => this._CuryPaymentAmt;
    set => this._CuryPaymentAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PaymentAmt
  {
    get => this._PaymentAmt;
    set => this._PaymentAmt = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXDefault]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXUIField(DisplayName = "Payment Method")]
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<SOInvoice.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<PX.Objects.AR.Customer.defPMInstanceID>>>>, Search<PX.Objects.AR.Customer.defPaymentMethodID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<SOInvoice.customerID>>>>>))]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.isActive, Equal<boolTrue>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<boolTrue>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
  public virtual string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.Customer.defPMInstanceID, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<PX.Objects.AR.Customer.defPMInstanceID>, And<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current2<SOInvoice.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current2<SOInvoice.paymentMethodID>>>>>>, Search2<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, LeftJoin<CCProcessingCenterPmntMethodBranch, On<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<CCProcessingCenterPmntMethodBranch.paymentMethodID>, And<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<CCProcessingCenterPmntMethodBranch.processingCenterID>, And<Current2<PX.Objects.AR.ARRegister.branchID>, Equal<CCProcessingCenterPmntMethodBranch.branchID>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current2<SOInvoice.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current2<SOInvoice.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>>>>, OrderBy<Asc<Switch<Case<Where<CCProcessingCenterPmntMethodBranch.paymentMethodID, IsNull>, True>, False>, Desc<PX.Objects.AR.CustomerPaymentMethod.expirationDate, Desc<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>>>))]
  [PXSelector(typeof (Search<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<SOInvoice.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current2<SOInvoice.paymentMethodID>>>>>), DescriptionField = typeof (PX.Objects.AR.CustomerPaymentMethod.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<boolTrue>, Or<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Current2<SOInvoice.pMInstanceID>>>>), "The {0} card/account number is inactive on the Customer Payment Methods (AR303010) form and cannot be processed.", new System.Type[] {typeof (PX.Objects.AR.CustomerPaymentMethod.descr)})]
  [DeprecatedProcessing]
  [DisabledProcCenter]
  public virtual int? PMInstanceID
  {
    get => this._PMInstanceID;
    set => this._PMInstanceID = value;
  }

  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.CustomerPaymentMethod.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.AR.CustomerPaymentMethod.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current2<SOInvoice.customerID>>, And<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Current2<SOInvoice.pMInstanceID>>>>>, Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.aRIsDefault, Equal<True>, And<PaymentMethodAccount.paymentMethodID, Equal<Current2<SOInvoice.paymentMethodID>>>>>>>, Where<PX.Objects.CA.CashAccount.branchID, Equal<Current<PX.Objects.AR.ARRegister.branchID>>, And<Match<Current<AccessInfo.userName>>>>>>))]
  [CashAccount(typeof (PX.Objects.AR.ARRegister.branchID), typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<Current2<SOInvoice.paymentMethodID>>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>, Where<Match<Current<AccessInfo.userName>>>>))]
  public virtual int? CashAccountID
  {
    get => this._CashAccountID;
    set => this._CashAccountID = value;
  }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Cleared")]
  [PXDefault(false)]
  public virtual bool? Cleared
  {
    get => this._Cleared;
    set => this._Cleared = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Clear Date")]
  public virtual DateTime? ClearDate
  {
    get => this._ClearDate;
    set => this._ClearDate = value;
  }

  [PXDBLong]
  [SOCashSaleCashTranID]
  public virtual long? CATranID
  {
    get => this._CATranID;
    set => this._CATranID = value;
  }

  [PXDBString(3, IsFixed = true, BqlField = typeof (SOInvoice.ARRegister.docType))]
  [PXRestriction]
  public virtual string ARRegisterDocType
  {
    get => (string) null;
    set
    {
    }
  }

  [PXDBString(15, IsUnicode = true, InputMask = "", BqlField = typeof (SOInvoice.ARRegister.refNbr))]
  [PXRestriction]
  public virtual string ARRegisterRefNbr
  {
    get => (string) null;
    set
    {
    }
  }

  [PXDBBool(BqlField = typeof (SOInvoice.ARRegister.released))]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARRegister.hold))]
  [PXDefault(false)]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXUIField(DisplayName = "Description")]
  [PXDBString(150, IsUnicode = true, BqlField = typeof (SOInvoice.ARRegister.docDesc))]
  public virtual string DocDesc
  {
    get => this._DocDesc;
    set => this._DocDesc = value;
  }

  [PXDBDate(BqlField = typeof (SOInvoice.ARRegister.docDate))]
  [PXUIField]
  public virtual DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (SOInvoice.ARRegister.status))]
  [PXUIField]
  [ARDocStatus.List]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARRegister.isTaxValid))]
  [PXDefault(false)]
  public virtual bool? IsTaxValid
  {
    get => this._IsTaxValid;
    set => this._IsTaxValid = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARRegister.isTaxSaved))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tax has been saved in the external tax provider", Enabled = false)]
  public virtual bool? IsTaxSaved { get; set; }

  [PXNote(DescriptionField = typeof (SOInvoice.refNbr), BqlField = typeof (SOInvoice.ARRegister.noteID))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (SOInvoice.curyInfoID), typeof (SOInvoice.origDocAmt), BqlField = typeof (PX.Objects.AR.ARRegister.curyOrigDocAmt))]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (PX.Objects.AR.ARRegister.origDocAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigDocAmt { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARRegister.disableAutomaticTaxCalculation))]
  public virtual bool? DisableAutomaticTaxCalculation { get; set; }

  [PXDBString(3, IsFixed = true, BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.docType))]
  [PXRestriction]
  public virtual string ARPaymentDocType
  {
    [PXDependsOnFields(new System.Type[] {typeof (SOInvoice.docType)})] get
    {
      return !(this._DocType == "CSL") && !(this._DocType == "RCS") ? (string) null : this._DocType;
    }
    set
    {
    }
  }

  [PXDBString(15, IsUnicode = true, InputMask = "", BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.refNbr))]
  [PXRestriction]
  public virtual string ARPaymentRefNbr
  {
    [PXDependsOnFields(new System.Type[] {typeof (SOInvoice.docType), typeof (SOInvoice.refNbr)})] get
    {
      return !(this._DocType == "CSL") && !(this._DocType == "RCS") ? (string) null : this._RefNbr;
    }
    set
    {
    }
  }

  [PXDBInt(BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.pMInstanceID))]
  public virtual int? ARPaymentPMInstanceID
  {
    get => this._PMInstanceID;
    set
    {
    }
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.paymentMethodID))]
  public virtual string ARPaymentPaymentMethodID
  {
    get => this._PaymentMethodID;
    set
    {
    }
  }

  [PXDBInt(BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.cashAccountID))]
  public virtual int? ARPaymentCashAccountID
  {
    get => this._CashAccountID;
    set
    {
    }
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.extRefNbr))]
  public virtual string ARPaymentExtRefNbr
  {
    get => this._ExtRefNbr;
    set
    {
    }
  }

  [PXDBDate(BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.adjDate))]
  public virtual DateTime? AdjDate
  {
    get => this._AdjDate;
    set => this._AdjDate = value;
  }

  [PXDBString(BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.adjFinPeriodID))]
  public virtual string AdjFinPeriodID
  {
    get => this._AdjFinPeriodID;
    set => this._AdjFinPeriodID = value;
  }

  [PXDBString(BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.adjTranPeriodID))]
  public virtual string AdjTranPeriodID
  {
    get => this._AdjTranPeriodID;
    set => this._AdjTranPeriodID = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.cleared))]
  public virtual bool? ARPaymentCleared
  {
    get => this._Cleared;
    set
    {
    }
  }

  [PXDBDate(BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.clearDate))]
  public virtual DateTime? ARPaymentClearDate
  {
    get => this._ClearDate;
    set
    {
    }
  }

  [PXDBLong(BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.cATranID))]
  public virtual long? ARPaymentCATranID
  {
    get => this._CATranID;
    set
    {
    }
  }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.depositAsBatch))]
  [PXDefault(false, typeof (Search<PX.Objects.CA.CashAccount.clearingAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<SOInvoice.cashAccountID>>>>))]
  [PXFormula(typeof (Default<SOInvoice.cashAccountID>))]
  public virtual bool? ARPaymentDepositAsBatch
  {
    get => this._DepositAsBatch;
    set => this._DepositAsBatch = value;
  }

  [PXDBDate(BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.depositAfter))]
  public virtual DateTime? DepositAfter
  {
    get => this._DepositAfter;
    set => this._DepositAfter = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.deposited))]
  [PXDefault(false)]
  public virtual bool? Deposited
  {
    get => this._Deposited;
    set => this._Deposited = value;
  }

  [PXDBString(3, IsFixed = true, BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.depositType))]
  public virtual string DepositType
  {
    get => this._DepositType;
    set => this._DepositType = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.depositNbr))]
  public virtual string DepositNbr
  {
    get => this._DepositNbr;
    set => this._DepositNbr = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.chargeCntr))]
  [PXDefault(0)]
  public virtual int? ChargeCntr
  {
    get => this._ChargeCntr;
    set => this._ChargeCntr = value;
  }

  [PXDBCurrency(typeof (SOInvoice.curyInfoID), typeof (SOInvoice.consolidateChargeTotal), BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.curyConsolidateChargeTotal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryConsolidateChargeTotal
  {
    get => this._CuryConsolidateChargeTotal;
    set => this._CuryConsolidateChargeTotal = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4, BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.consolidateChargeTotal))]
  public virtual Decimal? ConsolidateChargeTotal
  {
    get => this._ConsolidateChargeTotal;
    set => this._ConsolidateChargeTotal = value;
  }

  [ProjectDefault("AR")]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.visibleInAR, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase(BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.projectID))]
  public virtual int? PaymentProjectID
  {
    get => this._PaymentProjectID;
    set => this._PaymentProjectID = value;
  }

  [ActiveProjectTask(typeof (SOInvoice.paymentProjectID), "AR", DisplayName = "Project Task", BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.taskID))]
  public virtual int? PaymentTaskID
  {
    get => this._PaymentTaskID;
    set => this._PaymentTaskID = value;
  }

  /// <summary>
  /// The flag indicates that the Invoice contains at least one line with a Stock Item, therefore an Inventory Document should be created.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? CreateINDoc { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? TranCntr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType>), CacheGlobal = true)]
  public virtual string SOOrderType
  {
    get => this._SOOrderType;
    set => this._SOOrderType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<SOInvoice.sOOrderType>>>>))]
  public virtual string SOOrderNbr
  {
    get => this._SOOrderNbr;
    set => this._SOOrderNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  public virtual string InitialSOBehavior { get; set; }

  [PXBool]
  public virtual bool? SuggestRelatedItems { get; set; }

  public class PK : PrimaryKeyOf<SOInvoice>.By<SOInvoice.docType, SOInvoice.refNbr>
  {
    public static SOInvoice Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (SOInvoice) PrimaryKeyOf<SOInvoice>.By<SOInvoice.docType, SOInvoice.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<SOInvoice>.By<SOInvoice.customerID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<SOInvoice>.By<SOInvoice.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<SOInvoice>.By<SOInvoice.curyID>
    {
    }

    public class ARInvoice : 
      PrimaryKeyOf<PX.Objects.AR.ARInvoice>.By<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>.ForeignKeyOf<SOInvoice>.By<SOInvoice.docType, SOInvoice.refNbr>
    {
    }

    public class BillingAddress : 
      PrimaryKeyOf<SOBillingAddress>.By<SOBillingAddress.addressID>.ForeignKeyOf<SOInvoice>.By<SOInvoice.billAddressID>
    {
    }

    public class ShippingAddress : 
      PrimaryKeyOf<SOShippingAddress>.By<SOShippingAddress.addressID>.ForeignKeyOf<SOInvoice>.By<SOInvoice.shipAddressID>
    {
    }

    public class BillingContact : 
      PrimaryKeyOf<SOBillingContact>.By<SOBillingContact.contactID>.ForeignKeyOf<SOInvoice>.By<SOInvoice.billContactID>
    {
    }

    public class ShippingContact : 
      PrimaryKeyOf<SOShippingContact>.By<SOShippingContact.contactID>.ForeignKeyOf<SOInvoice>.By<SOInvoice.shipContactID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<SOInvoice>.By<SOInvoice.paymentMethodID>
    {
    }

    public class CustomerPaymentMethod : 
      PrimaryKeyOf<PX.Objects.AR.CustomerPaymentMethod>.By<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>.ForeignKeyOf<SOInvoice>.By<SOInvoice.pMInstanceID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<SOInvoice>.By<SOInvoice.cashAccountID>
    {
    }

    public class ARRegister : 
      PrimaryKeyOf<PX.Objects.AR.ARRegister>.By<PX.Objects.AR.ARRegister.docType, PX.Objects.AR.ARRegister.refNbr>.ForeignKeyOf<SOInvoice>.By<SOInvoice.aRRegisterDocType, SOInvoice.aRRegisterRefNbr>
    {
    }

    public class Payment : 
      PrimaryKeyOf<PX.Objects.AR.ARPayment>.By<PX.Objects.AR.ARPayment.docType, PX.Objects.AR.ARPayment.refNbr>.ForeignKeyOf<SOInvoice>.By<SOInvoice.aRPaymentDocType, SOInvoice.aRPaymentRefNbr>
    {
    }

    public class PaymentProject : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<SOInvoice>.By<SOInvoice.paymentProjectID>
    {
    }

    public class PaymentTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<SOInvoice>.By<SOInvoice.paymentProjectID, SOInvoice.paymentTaskID>
    {
    }
  }

  public class Events : PXEntityEventBase<SOInvoice>.Container<SOInvoice.Events>
  {
    public PXEntityEvent<SOInvoice> InvoiceReleased;
    public PXEntityEvent<SOInvoice> InvoiceCancelled;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOInvoice.selected>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOInvoice.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOInvoice.refNbr>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOInvoice.customerID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOInvoice.curyInfoID>
  {
  }

  public abstract class billAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOInvoice.billAddressID>
  {
  }

  public abstract class billContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOInvoice.billContactID>
  {
  }

  public abstract class shipAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOInvoice.shipAddressID>
  {
  }

  public abstract class shipContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOInvoice.shipContactID>
  {
  }

  public abstract class curyManDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOInvoice.curyManDisc>
  {
  }

  public abstract class manDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOInvoice.manDisc>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOInvoice.Tstamp>
  {
  }

  public abstract class curyPaymentAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOInvoice.curyPaymentAmt>
  {
  }

  public abstract class paymentAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOInvoice.paymentAmt>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOInvoice.curyID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOInvoice.paymentMethodID>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOInvoice.pMInstanceID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOInvoice.cashAccountID>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOInvoice.extRefNbr>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOInvoice.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOInvoice.clearDate>
  {
  }

  public abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOInvoice.cATranID>
  {
  }

  public abstract class aRRegisterDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOInvoice.aRRegisterDocType>
  {
  }

  public abstract class aRRegisterRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOInvoice.aRRegisterRefNbr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOInvoice.released>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOInvoice.hold>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOInvoice.docDesc>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOInvoice.docDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOInvoice.status>
  {
  }

  public abstract class isTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOInvoice.isTaxValid>
  {
  }

  public abstract class isTaxSaved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOInvoice.isTaxSaved>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOInvoice.noteID>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOInvoice.curyOrigDocAmt>
  {
  }

  public abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOInvoice.origDocAmt>
  {
  }

  public abstract class disableAutomaticTaxCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOInvoice.disableAutomaticTaxCalculation>
  {
  }

  public abstract class aRPaymentDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOInvoice.aRPaymentDocType>
  {
  }

  public abstract class aRPaymentRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOInvoice.aRPaymentRefNbr>
  {
  }

  public abstract class adjDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOInvoice.adjDate>
  {
  }

  public abstract class adjFinPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOInvoice.adjFinPeriodID>
  {
  }

  public abstract class adjTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOInvoice.adjTranPeriodID>
  {
  }

  public abstract class depositAsBatch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOInvoice.depositAsBatch>
  {
  }

  public abstract class depositAfter : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOInvoice.depositAfter>
  {
  }

  public abstract class deposited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOInvoice.deposited>
  {
  }

  public abstract class depositType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOInvoice.depositType>
  {
  }

  public abstract class depositNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOInvoice.depositNbr>
  {
  }

  public abstract class chargeCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOInvoice.chargeCntr>
  {
  }

  public abstract class curyConsolidateChargeTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOInvoice.curyConsolidateChargeTotal>
  {
  }

  public abstract class consolidateChargeTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOInvoice.consolidateChargeTotal>
  {
  }

  public abstract class paymentProjectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOInvoice.paymentProjectID>
  {
  }

  public abstract class paymentTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOInvoice.paymentTaskID>
  {
  }

  public abstract class createINDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOInvoice.createINDoc>
  {
  }

  public abstract class tranCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOInvoice.tranCntr>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOInvoice.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOInvoice.sOOrderNbr>
  {
  }

  public abstract class initialSOBehavior : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOInvoice.initialSOBehavior>
  {
  }

  public abstract class suggestRelatedItems : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOInvoice.suggestRelatedItems>
  {
  }

  [PXHidden]
  [Serializable]
  public class ARRegister : PX.Objects.AR.ARRegister
  {
    [PXDBString(1, IsFixed = true)]
    public override string Status
    {
      get => this._Status;
      set => this._Status = value;
    }

    [PXDBLong]
    public override long? CuryInfoID
    {
      get => this._CuryInfoID;
      set => this._CuryInfoID = value;
    }

    public new abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOInvoice.ARRegister.docType>
    {
    }

    public new abstract class refNbr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOInvoice.ARRegister.refNbr>
    {
    }

    public new abstract class docDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOInvoice.ARRegister.docDesc>
    {
    }

    public new abstract class released : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOInvoice.ARRegister.released>
    {
    }

    public new abstract class noteID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOInvoice.ARRegister.noteID>
    {
    }

    public new abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      SOInvoice.ARRegister.docDate>
    {
    }

    public new abstract class status : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOInvoice.ARRegister.status>
    {
    }

    public new abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      SOInvoice.ARRegister.curyInfoID>
    {
    }
  }
}
