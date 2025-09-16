// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PaymentTransaction.PaymentTransactionDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using System;

#nullable enable
namespace PX.Objects.Extensions.PaymentTransaction;

public class PaymentTransactionDetail : PXMappedCacheExtension, ICCPaymentTransaction
{
  public virtual int? TranNbr { get; set; }

  public virtual int? TransactionID { get; set; }

  public virtual int? PMInstanceID { get; set; }

  public virtual 
  #nullable disable
  string ProcessingCenterID { get; set; }

  public virtual string DocType { get; set; }

  public virtual string RefNbr { get; set; }

  public int? RefTranNbr { get; set; }

  public virtual string OrigDocType { get; set; }

  public virtual string OrigRefNbr { get; set; }

  public virtual string TranType { get; set; }

  public virtual string ProcStatus { get; set; }

  public virtual string TranStatus { get; set; }

  public virtual System.DateTime? ExpirationDate { get; set; }

  public string PCTranNumber { get; set; }

  /// <exclude />
  public string PCTranApiNumber { get; set; }

  public string AuthNumber { get; set; }

  public string PCResponseReasonText { get; set; }

  public Decimal? Amount { get; set; }

  public bool? Imported { get; set; }

  public abstract class tranNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PaymentTransactionDetail.tranNbr>
  {
  }

  public abstract class transactionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PaymentTransactionDetail.transactionID>
  {
  }

  public abstract class pMInstanceID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PaymentTransactionDetail.pMInstanceID>
  {
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentTransactionDetail.processingCenterID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentTransactionDetail.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentTransactionDetail.refNbr>
  {
  }

  public abstract class refTranNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PaymentTransactionDetail.refTranNbr>
  {
  }

  public abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentTransactionDetail.origDocType>
  {
  }

  public abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentTransactionDetail.origRefNbr>
  {
  }

  public abstract class tranType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentTransactionDetail.tranType>
  {
  }

  public abstract class procStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentTransactionDetail.procStatus>
  {
  }

  public abstract class tranStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentTransactionDetail.tranStatus>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    PaymentTransactionDetail.expirationDate>
  {
  }

  public abstract class pCTranNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentTransactionDetail.pCTranNumber>
  {
  }

  public abstract class pCTranApiNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentTransactionDetail.pCTranApiNumber>
  {
  }

  public abstract class authNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentTransactionDetail.authNumber>
  {
  }

  public abstract class pCResponseReasonText : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentTransactionDetail.pCResponseReasonText>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PaymentTransactionDetail.amount>
  {
  }

  public abstract class imported : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentTransactionDetail.imported>
  {
  }
}
