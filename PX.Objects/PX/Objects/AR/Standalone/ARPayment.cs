// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Standalone.ARPayment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.AR.Standalone;

[PXHidden(ServiceVisible = false)]
[PXCacheName("AR Payment")]
[Serializable]
public class ARPayment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected int? _PMInstanceID;
  protected string _PaymentMethodID;
  protected int? _CashAccountID;
  protected string _ExtRefNbr;
  protected DateTime? _AdjDate;
  protected string _AdjFinPeriodID;
  protected string _AdjTranPeriodID;
  protected bool? _Cleared;
  protected DateTime? _ClearDate;
  protected long? _CATranID;
  protected bool? _DepositAsBatch;
  protected DateTime? _DepositAfter;
  protected DateTime? _DepositDate;
  protected bool? _Deposited;
  protected string _DepositType;
  protected string _DepositNbr;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected int? _ChargeCntr;
  protected Decimal? _CuryConsolidateChargeTotal;
  protected Decimal? _ConsolidateChargeTotal;
  protected string _RefTranExtNbr;

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt]
  public virtual int? PMInstanceID
  {
    get => this._PMInstanceID;
    set => this._PMInstanceID = value;
  }

  /// <summary>Terminal ID</summary>
  [PXDBString(36, IsUnicode = true, InputMask = "")]
  public virtual string TerminalID { get; set; }

  /// <summary>Indicates whether the payment is made in card-present mode</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? CardPresent { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDBDefault]
  [PXSelector(typeof (Search<CCProcessingCenter.processingCenterID>))]
  public virtual string ProcessingCenterID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string PaymentMethodID
  {
    get => this._PaymentMethodID;
    set => this._PaymentMethodID = value;
  }

  [PXDBInt]
  public virtual int? CashAccountID
  {
    get => this._CashAccountID;
    set => this._CashAccountID = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal]
  public virtual Decimal? CuryOrigTaxDiscAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal]
  public virtual Decimal? OrigTaxDiscAmt { get; set; }

  [PXDBString(40, IsUnicode = true)]
  public virtual string ExtRefNbr
  {
    get => this._ExtRefNbr;
    set => this._ExtRefNbr = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? AdjDate
  {
    get => this._AdjDate;
    set => this._AdjDate = value;
  }

  [PXDBString]
  public virtual string AdjFinPeriodID
  {
    get => this._AdjFinPeriodID;
    set => this._AdjFinPeriodID = value;
  }

  [PXDBString]
  public virtual string AdjTranPeriodID
  {
    get => this._AdjTranPeriodID;
    set => this._AdjTranPeriodID = value;
  }

  [PXDBBool]
  public virtual bool? Cleared
  {
    get => this._Cleared;
    set => this._Cleared = value;
  }

  [PXDBDate]
  public virtual DateTime? ClearDate
  {
    get => this._ClearDate;
    set => this._ClearDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Settled")]
  public virtual bool? Settled { get; set; }

  [PXDBLong]
  public virtual long? CATranID
  {
    get => this._CATranID;
    set => this._CATranID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DepositAsBatch
  {
    get => this._DepositAsBatch;
    set => this._DepositAsBatch = value;
  }

  [PXDBDate]
  [PXDefault]
  public virtual DateTime? DepositAfter
  {
    get => this._DepositAfter;
    set => this._DepositAfter = value;
  }

  [PXDate]
  public virtual DateTime? DepositDate
  {
    get => this._DepositDate;
    set => this._DepositDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
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
  public virtual string DepositNbr
  {
    get => this._DepositNbr;
    set => this._DepositNbr = value;
  }

  [ProjectDefault("AR")]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.visibleInAR, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<ARPayment.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ActiveProjectTask(typeof (ARPayment.projectID), "AR", DisplayName = "Project Task")]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ChargeCntr
  {
    get => this._ChargeCntr;
    set => this._ChargeCntr = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryConsolidateChargeTotal
  {
    get => this._CuryConsolidateChargeTotal;
    set => this._CuryConsolidateChargeTotal = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ConsolidateChargeTotal
  {
    get => this._ConsolidateChargeTotal;
    set => this._ConsolidateChargeTotal = value;
  }

  [PXDBString(50, IsUnicode = true)]
  [PXDefault]
  public virtual string RefTranExtNbr
  {
    get => this._RefTranExtNbr;
    set => this._RefTranExtNbr = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARPayment.isCCPayment))]
  [PXDefault(false)]
  public virtual bool? IsCCPayment { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARPayment.isCCAuthorized))]
  [PXDefault(false)]
  public virtual bool? IsCCAuthorized { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARPayment.isCCCaptured))]
  [PXDefault(false)]
  public virtual bool? IsCCCaptured { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARPayment.isCCCaptureFailed))]
  [PXDefault(false)]
  public virtual bool? IsCCCaptureFailed { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARPayment.isCCRefunded))]
  [PXDefault(false)]
  public virtual bool? IsCCRefunded { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.ARPayment.isCCUserAttention))]
  [PXDefault(false)]
  public virtual bool? IsCCUserAttention { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.AR.ARPayment.cCActualExternalTransactionID))]
  public virtual int? CCActualExternalTransactionID { get; set; }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.refNbr>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.pMInstanceID>
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

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPayment.processingCenterID>
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

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.cashAccountID>
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

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.extRefNbr>
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

  public abstract class settled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARPayment.settled>
  {
  }

  public abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARPayment.cATranID>
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

  public abstract class chargeCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPayment.chargeCntr>
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

  public abstract class refTranExtNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPayment.refTranExtNbr>
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

  public abstract class cCActualExternalTransactionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARPayment.cCActualExternalTransactionID>
  {
  }
}
