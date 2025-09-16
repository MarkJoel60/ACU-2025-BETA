// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PaymentTransaction.Payment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.CCPaymentProcessing.Common;
using System;

#nullable enable
namespace PX.Objects.Extensions.PaymentTransaction;

public class Payment : PXMappedCacheExtension, ICCPayment
{
  public int? PMInstanceID { get; set; }

  public 
  #nullable disable
  string PaymentMethodID { get; set; }

  public string ProcessingCenterID { get; set; }

  public int? CashAccountID { get; set; }

  public Decimal? CuryDocBal { get; set; }

  public string CuryID { get; set; }

  public string DocType { get; set; }

  public string RefNbr { get; set; }

  public string OrigDocType { get; set; }

  public string OrigRefNbr { get; set; }

  public string RefTranExtNbr { get; set; }

  public bool? Released { get; set; }

  public bool? SaveCard { get; set; }

  public bool? CCTransactionRefund { get; set; }

  public string CCPaymentStateDescr { get; set; }

  public int? CCActualExternalTransactionID { get; set; }

  /// <summary>Amount before tax.</summary>
  public Decimal? SubtotalAmount { get; set; }

  /// <summary>Total tax amount.</summary>
  public Decimal? Tax { get; set; }

  /// <summary>Level 3 Data for processing.</summary>
  public TranProcessingL3DataInput L3Data { get; set; }

  /// <summary>Terminal ID</summary>
  public string TerminalID { get; set; }

  /// <summary>If true, indicates that the transaction is in card-present mode.</summary>
  public bool? CardPresent { get; set; }

  /// <summary>
  /// New amount of payment for increase operation (in a currency of payment)
  /// </summary>
  public Decimal? CuryDocBalIncrease { get; set; }

  /// <summary>
  /// New amount applied to document with OrigDocType and OrigRefNbr if increase amount operation is successful
  /// </summary>
  public Decimal? OrigDocAppliedAmount { get; set; }

  /// <summary>Type of doc raised CC payment transaction</summary>
  public string TransactionOrigDocType { get; set; }

  /// <summary>Reference number of doc raised CC payment transaction</summary>
  public string TransactionOrigDocRefNbr { get; set; }

  /// <summary>
  /// Is rejection (for recording eft rejection from settlement)
  /// </summary>
  public bool? IsRejection { get; set; }

  /// <summary>
  /// Previous external transaction id (for AuthorizeBasedOnPrevious operation)
  /// </summary>
  public string PreviousExternalTransactionID { get; set; }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Payment.pMInstanceID>
  {
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Payment.processingCenterID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Payment.cashAccountID>
  {
  }

  public abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Payment.curyDocBal>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Payment.curyID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Payment.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Payment.refNbr>
  {
  }

  public abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Payment.origDocType>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Payment.origRefNbr>
  {
  }

  public abstract class refTranExtNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Payment.refTranExtNbr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Payment.released>
  {
  }

  public abstract class saveCard : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Payment.saveCard>
  {
  }

  public abstract class cCTransactionRefund : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Payment.cCTransactionRefund>
  {
  }

  public abstract class cCPaymentStateDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Payment.cCPaymentStateDescr>
  {
  }

  public abstract class cCActualExternalTransactionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Payment.cCActualExternalTransactionID>
  {
  }

  public abstract class subtotalAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Payment.subtotalAmount>
  {
  }

  public abstract class tax : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Payment.tax>
  {
  }

  public abstract class l3Data : IBqlField, IBqlOperand
  {
  }

  public abstract class terminalID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Payment.terminalID>
  {
  }
}
