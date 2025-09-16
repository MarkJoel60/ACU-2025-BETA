// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCProcTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.CA;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// A credit card processing transaction of an accounts
/// receivable payment or a cash sale. They are visible on the Credit
/// Card Processing Info tab of the Payments and Applications (AR302000)
/// and Cash Sales (AR304000) forms, which correspond to the <see cref="T:PX.Objects.AR.ARPaymentEntry" /> and <see cref="T:PX.Objects.AR.ARCashSaleEntry" /> graphs,
/// respectively.
/// </summary>
[PXCacheName("Credit Card Processing Transaction")]
[Serializable]
public class CCProcTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ICCPaymentTransaction
{
  protected int? _TranNbr;
  protected int? _PMInstanceID;
  protected 
  #nullable disable
  string _ProcessingCenterID;
  protected string _DocType;
  protected string _RefNbr;
  protected string _OrigDocType;
  protected string _OrigRefNbr;
  protected string _TranType;
  protected string _ProcStatus;
  protected string _TranStatus;
  protected string _CVVVerificationStatus;
  protected string _CuryID;
  protected Decimal? _Amount;
  protected int? _RefTranNbr;
  protected string _RefPCTranNumber;
  protected string _PCTranNumber;
  protected string _PCTranApiNumber;
  protected string _AuthNumber;
  protected string _PCResponseCode;
  protected string _PCResponseReasonCode;
  protected string _PCResponseReasonText;
  protected string _PCResponse;
  protected DateTime? _StartTime;
  protected DateTime? _EndTime;
  protected DateTime? _ExpirationDate;
  protected string _ErrorSource;
  protected string _ErrorText;
  protected byte[] _tstamp;

  [PXDBIdentity(IsKey = true)]
  [PXUIField]
  public virtual int? TranNbr
  {
    get => this._TranNbr;
    set => this._TranNbr = value;
  }

  [PXDBInt]
  [PXDBDefault(typeof (ExternalTransaction.transactionID))]
  [PXParent(typeof (Select<ExternalTransaction, Where<ExternalTransaction.transactionID, Equal<Current<CCProcTran.transactionID>>>>))]
  [PXUIField]
  public virtual int? TransactionID { get; set; }

  [PXDBInt]
  public virtual int? PMInstanceID
  {
    get => this._PMInstanceID;
    set => this._PMInstanceID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("")]
  [PXUIField]
  public virtual string ProcessingCenterID
  {
    get => this._ProcessingCenterID;
    set => this._ProcessingCenterID = value;
  }

  [PXDBString(3)]
  [PXUIField]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBString(3)]
  [PXUIField(DisplayName = "Orig. Doc. Type")]
  public virtual string OrigDocType
  {
    get => this._OrigDocType;
    set => this._OrigDocType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Orig. Doc. Ref. Nbr.")]
  public virtual string OrigRefNbr
  {
    get => this._OrigRefNbr;
    set => this._OrigRefNbr = value;
  }

  [PXDBString(3, IsFixed = true)]
  [PXDefault]
  [CCTranTypeCode.List]
  [PXUIField(DisplayName = "Tran. Type")]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXDBString(3, IsFixed = true)]
  [PXDefault("OPN")]
  [CCProcStatus.List]
  [PXUIField(DisplayName = "Proc. Status")]
  public virtual string ProcStatus
  {
    get => this._ProcStatus;
    set => this._ProcStatus = value;
  }

  [PXDBString(3, IsFixed = true)]
  [CCTranStatusCode.List]
  [PXUIField(DisplayName = "Tran. Status")]
  public virtual string TranStatus
  {
    get => this._TranStatus;
    set => this._TranStatus = value;
  }

  [PXDBString(3, IsFixed = true)]
  [PXDefault("NOV")]
  [CVVVerificationStatusCode.List]
  [PXUIField(DisplayName = "CVV Verification")]
  public virtual string CVVVerificationStatus
  {
    get => this._CVVVerificationStatus;
    set => this._CVVVerificationStatus = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? Amount
  {
    get => this._Amount;
    set => this._Amount = value;
  }

  /// <summary>Amount before tax.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? SubtotalAmount { get; set; }

  /// <summary>Total tax amount.</summary>
  [PXDBDecimal(4)]
  [PXUIField]
  public virtual Decimal? Tax { get; set; }

  [PXDateAndTime]
  [PXFormula(typeof (Switch<Case<Where<CCProcTran.tranType, Equal<CCTranTypeCode.authorize>>, Parent<ExternalTransaction.fundHoldExpDate>>>))]
  [PXUIField]
  public virtual DateTime? FundHoldExpDate { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Referenced Tran. Nbr.")]
  public virtual int? RefTranNbr
  {
    get => this._RefTranNbr;
    set => this._RefTranNbr = value;
  }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Proc. Center Ref. Tran. Nbr.")]
  public virtual string RefPCTranNumber
  {
    get => this._RefPCTranNumber;
    set => this._RefPCTranNumber = value;
  }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  public virtual string PCTranNumber
  {
    get => this._PCTranNumber;
    set => this._PCTranNumber = value;
  }

  /// <summary>
  /// Transaction identifier assigned by an external service other than the processing center.
  /// </summary>
  [PXString(50, IsUnicode = true)]
  public virtual string PCTranApiNumber
  {
    get => this._PCTranApiNumber;
    set => this._PCTranApiNumber = value;
  }

  /// <summary>
  /// Payment identifier assigned by an external ecommerce system.
  /// </summary>
  [PXString(50, IsUnicode = true)]
  public virtual string CommerceTranNumber { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Proc. Center Auth. Nbr.")]
  public virtual string AuthNumber
  {
    get => this._AuthNumber;
    set => this._AuthNumber = value;
  }

  /// <summary>Terminal ID</summary>
  [PXString(36, IsUnicode = true)]
  [PXUIField(DisplayName = "Terminal ID")]
  public virtual string TerminalID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string PCResponseCode
  {
    get => this._PCResponseCode;
    set => this._PCResponseCode = value;
  }

  [PXDBString(20, IsUnicode = true)]
  public virtual string PCResponseReasonCode
  {
    get => this._PCResponseReasonCode;
    set => this._PCResponseReasonCode = value;
  }

  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Proc. Center Response Reason")]
  public virtual string PCResponseReasonText
  {
    get => this._PCResponseReasonText;
    set => this._PCResponseReasonText = value;
  }

  [PXRSACryptString(2048 /*0x0800*/, IsUnicode = true)]
  public virtual string PCResponse
  {
    get => this._PCResponse;
    set => this._PCResponse = value;
  }

  [PXDBDate(PreserveTime = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tran. Time")]
  public virtual DateTime? StartTime
  {
    get => this._StartTime;
    set => this._StartTime = value;
  }

  [PXDBDate(PreserveTime = true)]
  public virtual DateTime? EndTime
  {
    get => this._EndTime;
    set => this._EndTime = value;
  }

  [PXDBDate(PreserveTime = true)]
  public virtual DateTime? ExpirationDate
  {
    get => this._ExpirationDate;
    set => this._ExpirationDate = value;
  }

  [PXDBString(3, IsFixed = true)]
  [PXUIField(Visible = false, DisplayName = "Error Source")]
  public virtual string ErrorSource
  {
    get => this._ErrorSource;
    set => this._ErrorSource = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(Visible = false, DisplayName = "Error Text")]
  public virtual string ErrorText
  {
    get => this._ErrorText;
    set => this._ErrorText = value;
  }

  /// <summary>Masked card number.</summary>
  [PXString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Card/Account Nbr.", Enabled = false)]
  [PXUnboundDefault(TypeCode.String, "")]
  public string MaskedCardNumber { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Imported")]
  public virtual bool? Imported { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public virtual void Copy(ICCPayment aPmtInfo)
  {
    this.PMInstanceID = aPmtInfo.PMInstanceID;
    this.DocType = aPmtInfo.DocType;
    this.RefNbr = aPmtInfo.RefNbr;
    this.CuryID = aPmtInfo.CuryID;
    this.Amount = aPmtInfo.CuryDocBal;
    this.OrigDocType = aPmtInfo.OrigDocType;
    this.OrigRefNbr = aPmtInfo.OrigRefNbr;
    this.TerminalID = aPmtInfo.TerminalID;
    this.ProcessingCenterID = aPmtInfo.ProcessingCenterID;
  }

  public virtual bool IsManuallyEntered()
  {
    return this.ProcStatus == "FIN" && this.TranStatus == "APR" && string.IsNullOrEmpty(this.PCResponseCode);
  }

  public class PK : PrimaryKeyOf<CCProcTran>.By<CCProcTran.tranNbr>
  {
    public static CCProcTran Find(PXGraph graph, int? tranNbr, PKFindOptions options = 0)
    {
      return (CCProcTran) PrimaryKeyOf<CCProcTran>.By<CCProcTran.tranNbr>.FindBy(graph, (object) tranNbr, options);
    }
  }

  public static class FK
  {
    public class ExternalTransaction : 
      PrimaryKeyOf<ExternalTransaction>.By<ExternalTransaction.transactionID>.ForeignKeyOf<CCProcTran>.By<CCProcTran.transactionID>
    {
    }

    public class CustomerPaymentMethod : 
      PrimaryKeyOf<CustomerPaymentMethod>.By<CustomerPaymentMethod.pMInstanceID>.ForeignKeyOf<CCProcTran>.By<CCProcTran.pMInstanceID>
    {
    }

    public class ProcessingCenter : 
      PrimaryKeyOf<CCProcessingCenter>.By<CCProcessingCenter.processingCenterID>.ForeignKeyOf<CCProcTran>.By<CCProcTran.processingCenterID>
    {
    }

    public class ARPayment : 
      PrimaryKeyOf<ARPayment>.By<ARPayment.docType, ARPayment.refNbr>.ForeignKeyOf<CCProcTran>.By<CCProcTran.docType, CCProcTran.refNbr>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CCProcTran>.By<CCProcTran.curyID>
    {
    }

    public class ParentProcessingTransaction : 
      PrimaryKeyOf<CCProcTran>.By<CCProcTran.tranNbr>.ForeignKeyOf<CCProcTran>.By<CCProcTran.refTranNbr>
    {
    }
  }

  public abstract class tranNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCProcTran.tranNbr>
  {
  }

  public abstract class transactionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCProcTran.transactionID>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCProcTran.pMInstanceID>
  {
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcTran.processingCenterID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcTran.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcTran.refNbr>
  {
  }

  public abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcTran.origDocType>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcTran.origRefNbr>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcTran.tranType>
  {
  }

  public abstract class procStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcTran.procStatus>
  {
  }

  public abstract class tranStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcTran.tranStatus>
  {
  }

  public abstract class cVVVerificationStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcTran.cVVVerificationStatus>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcTran.curyID>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CCProcTran.amount>
  {
  }

  public abstract class subtotalAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CCProcTran.subtotalAmount>
  {
  }

  public abstract class tax : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CCProcTran.tax>
  {
  }

  public abstract class fundHoldExpDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcTran.fundHoldExpDate>
  {
  }

  public abstract class refTranNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCProcTran.refTranNbr>
  {
  }

  public abstract class refPCTranNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcTran.refPCTranNumber>
  {
  }

  public abstract class pCTranNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcTran.pCTranNumber>
  {
  }

  public abstract class pCTranApiNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcTran.pCTranApiNumber>
  {
  }

  public abstract class commerceTranNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcTran.commerceTranNumber>
  {
  }

  public abstract class authNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcTran.authNumber>
  {
  }

  public abstract class terminalID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcTran.terminalID>
  {
  }

  public abstract class pCResponseCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcTran.pCResponseCode>
  {
  }

  public abstract class pCResponseReasonCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcTran.pCResponseReasonCode>
  {
  }

  public abstract class pCResponseReasonText : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcTran.pCResponseReasonText>
  {
  }

  public abstract class pCResponse : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcTran.pCResponse>
  {
  }

  public abstract class startTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CCProcTran.startTime>
  {
  }

  public abstract class endTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CCProcTran.endTime>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcTran.expirationDate>
  {
  }

  public abstract class errorSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcTran.errorSource>
  {
  }

  public abstract class errorText : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcTran.errorText>
  {
  }

  public abstract class maskedCardNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcTran.maskedCardNumber>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CCProcTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcTran.createdDateTime>
  {
  }

  public abstract class imported : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CCProcTran.imported>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CCProcTran.Tstamp>
  {
  }
}
