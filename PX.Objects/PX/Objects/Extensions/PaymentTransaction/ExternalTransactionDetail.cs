// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PaymentTransaction.ExternalTransactionDetail
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

[PXHidden]
public class ExternalTransactionDetail : PXMappedCacheExtension, IExternalTransaction
{
  public int? TransactionID { get; set; }

  public int? PMInstanceID { get; set; }

  public 
  #nullable disable
  string DocType { get; set; }

  public string RefNbr { get; set; }

  public string OrigDocType { get; set; }

  public string OrigRefNbr { get; set; }

  public string VoidDocType { get; set; }

  public string VoidRefNbr { get; set; }

  public string TranNumber { get; set; }

  public string TranApiNumber { get; set; }

  public string CommerceTranNumber { get; set; }

  public string AuthNumber { get; set; }

  public Decimal? Amount { get; set; }

  public string ProcStatus { get; set; }

  public System.DateTime? LastActivityDate { get; set; }

  public string Direction { get; set; }

  public bool? Active { get; set; }

  public bool? Completed { get; set; }

  public bool? NeedSync { get; set; }

  public bool? SaveProfile { get; set; }

  public int? ParentTranID { get; set; }

  public System.DateTime? ExpirationDate { get; set; }

  public string CVVVerification { get; set; }

  public string ProcessingCenterID { get; set; }

  public string SyncStatus { get; set; }

  public string SyncMessage { get; set; }

  public Guid? NoteID { get; set; }

  public string LastDigits { get; set; }

  public string CardType { get; set; }

  public abstract class transactionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ExternalTransactionDetail.transactionID>
  {
  }

  public abstract class pMInstanceID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ExternalTransactionDetail.pMInstanceID>
  {
  }

  public abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExternalTransactionDetail.refNbr>
  {
  }

  public abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.origDocType>
  {
  }

  public abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.origRefNbr>
  {
  }

  public abstract class voidDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.voidDocType>
  {
  }

  public abstract class voidRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.voidRefNbr>
  {
  }

  public abstract class tranNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.tranNumber>
  {
  }

  public abstract class tranApiNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.tranApiNumber>
  {
  }

  public abstract class commerceTranNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.commerceTranNumber>
  {
  }

  public abstract class authNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.authNumber>
  {
  }

  public abstract class amount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ExternalTransactionDetail.amount>
  {
  }

  public abstract class procStatus : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ExternalTransactionDetail.procStatus>
  {
  }

  public abstract class lastActivityDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    ExternalTransactionDetail.lastActivityDate>
  {
  }

  public abstract class direction : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.direction>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ExternalTransactionDetail.active>
  {
  }

  public abstract class completed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ExternalTransactionDetail.completed>
  {
  }

  public abstract class needSync : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ExternalTransactionDetail.needSync>
  {
  }

  public abstract class saveProfile : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ExternalTransactionDetail.saveProfile>
  {
  }

  public abstract class parentTranID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ExternalTransactionDetail.parentTranID>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    ExternalTransactionDetail.expirationDate>
  {
  }

  public abstract class cVVVerification : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.cVVVerification>
  {
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.processingCenterID>
  {
  }

  public abstract class syncStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.syncStatus>
  {
  }

  public abstract class syncMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.syncMessage>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExternalTransactionDetail.noteID>
  {
  }

  public abstract class lastDigits : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.lastDigits>
  {
  }

  public abstract class cardType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransactionDetail.cardType>
  {
  }
}
