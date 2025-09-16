// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCBatchTransaction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Bql;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("CCBatchTransaction")]
[Serializable]
public class CCBatchTransaction : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public bool? SelectedToHide { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public bool? SelectedToUnhide { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Batch ID")]
  [PXParent(typeof (Select<CCBatch, Where<CCBatch.batchID, Equal<Current<CCBatchTransaction.batchID>>>>))]
  [PXFormula(null, typeof (CountCalc<CCBatch.importedTransactionCount>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<CCBatchTransaction.processingStatus, Equal<CCBatchTranProcessingStatusCode.pendingProcessing>>, int1>, int0>), typeof (SumCalc<CCBatch.unprocessedCount>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<CCBatchTransaction.processingStatus, Equal<CCBatchTranProcessingStatusCode.missing>>, int1>, int0>), typeof (SumCalc<CCBatch.missingCount>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<CCBatchTransaction.processingStatus, Equal<CCBatchTranProcessingStatusCode.hidden>>, int1>, int0>), typeof (SumCalc<CCBatch.hiddenCount>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<CCBatchTransaction.processingStatus, Equal<CCBatchTranProcessingStatusCode.processed>>, int1>, int0>), typeof (SumCalc<CCBatch.processedCount>))]
  [PXDBDefault(typeof (CCBatch.batchID))]
  public virtual int? BatchID { get; set; }

  [PXDBString(50, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Proc. Center Tran. Nbr.")]
  public virtual 
  #nullable disable
  string PCTranNumber { get; set; }

  [PXDBString(40, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Proc. Center Customer ID")]
  public virtual string PCCustomerID { get; set; }

  [PXDBString(40, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Proc. Center Profile ID")]
  public virtual string PCPaymentProfileID { get; set; }

  [PXDBString(3, IsFixed = true)]
  [CCBatchTranSettlementStatusCode.List]
  [PXUIField(DisplayName = "Settlement Status")]
  public virtual string SettlementStatus { get; set; }

  [PXDBString(40, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Invoice Nbr")]
  public virtual string InvoiceNbr { get; set; }

  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "Submit Time")]
  public virtual DateTime? SubmitTime { get; set; }

  /// <summary>
  /// Original card type value received from the processing center.
  /// </summary>
  [PXDBString(25, IsFixed = true)]
  [PXUIField(DisplayName = "Proc. Center Card Type", Enabled = false)]
  public virtual string ProcCenterCardTypeCode { get; set; }

  /// <summary>
  /// Type of a card associated with the customer payment method.
  /// </summary>
  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "Card Type", Enabled = false)]
  [CardType.List]
  public virtual string CardTypeCode { get; set; }

  /// <summary>
  /// Specifies display card type value.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXString(20, IsFixed = true)]
  [PXUIField(DisplayName = "Card Type", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<CCBatchTransaction.cardTypeCode, IsNull>, CCBatchTransaction.procCenterCardTypeCode, Case<Where<CCBatchTransaction.cardTypeCode, Equal<CardType.other>, And<CCBatchTransaction.procCenterCardTypeCode, IsNotNull>>, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<ListLabelOf<CCBatchTransaction.cardTypeCode>.Evaluator, Colon>>, IBqlString>.Concat<CCBatchTransaction.procCenterCardTypeCode>>>, ListLabelOf<CCBatchTransaction.cardTypeCode>>))]
  public virtual string DisplayCardType { get; set; }

  [PXDBString(20, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  public virtual string AccountNumber { get; set; }

  [PXDBCury(typeof (CCBatch.curyID))]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? Amount { get; set; }

  [PXDBCury(typeof (CCBatch.curyID))]
  [PXUIField(DisplayName = "Fixed Fee")]
  public virtual Decimal? FixedFee { get; set; }

  [PXDBCury(typeof (CCBatch.curyID))]
  [PXUIField(DisplayName = "Percentage Fee")]
  public virtual Decimal? PercentageFee { get; set; }

  [PXCury(typeof (CCBatch.curyID))]
  [PXDBCalced(typeof (Add<IsNull<CCBatchTransaction.fixedFee, decimal0>, IsNull<CCBatchTransaction.percentageFee, decimal0>>), typeof (Decimal))]
  [PXUIField(DisplayName = "Total Fee")]
  public virtual Decimal? TotalFee { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Fee Type")]
  public virtual string FeeType { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Transaction ID")]
  public virtual int? TransactionID { get; set; }

  [PXDBString(3, IsFixed = true)]
  [ExtTransactionProcStatusCode.List]
  [PXUIField(DisplayName = "Original Status")]
  public virtual string OriginalStatus { get; set; }

  [PXDBString(3, IsFixed = true, InputMask = "")]
  [PXUIField(DisplayName = "Current Status")]
  public virtual string CurrentStatus { get; set; }

  [PXDBString(3, IsFixed = true)]
  [CCBatchTranProcessingStatusCode.List]
  [PXUIField(DisplayName = "Processing Status")]
  public virtual string ProcessingStatus { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXUIField]
  [ARDocType.List]
  public virtual string DocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<ARRegister.refNbr, Where<ARRegister.docType, Equal<Optional<CCBatchTransaction.docType>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "Tstamp")]
  public virtual byte[] Tstamp { get; set; }

  [PXNote]
  public virtual Guid? Noteid { get; set; }

  public class PK : 
    PrimaryKeyOf<CCBatchTransaction>.By<CCBatchTransaction.batchID, CCBatchTransaction.pCTranNumber>
  {
    public static CCBatchTransaction Find(
      PXGraph graph,
      int? batchID,
      string pCTranNumber,
      PKFindOptions options = 0)
    {
      return (CCBatchTransaction) PrimaryKeyOf<CCBatchTransaction>.By<CCBatchTransaction.batchID, CCBatchTransaction.pCTranNumber>.FindBy(graph, (object) batchID, (object) pCTranNumber, options);
    }
  }

  public static class FK
  {
    public class CCBatch : 
      PrimaryKeyOf<CCBatch>.By<CCBatch.batchID>.ForeignKeyOf<CCBatchTransaction>.By<CCBatchTransaction.batchID>
    {
    }

    public class ARPayment : 
      PrimaryKeyOf<PX.Objects.AR.ARPayment>.By<PX.Objects.AR.ARPayment.docType, PX.Objects.AR.ARPayment.refNbr>.ForeignKeyOf<CCBatchTransaction>.By<CCBatchTransaction.docType, CCBatchTransaction.refNbr>
    {
    }

    public class ExternalTransaction : 
      PrimaryKeyOf<PX.Objects.AR.ExternalTransaction>.By<PX.Objects.AR.ExternalTransaction.transactionID>.ForeignKeyOf<CCBatchTransaction>.By<CCBatchTransaction.transactionID>
    {
    }
  }

  public abstract class selectedToHide : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCBatchTransaction.selectedToHide>
  {
  }

  public abstract class selectedToUnhide : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCBatchTransaction.selectedToUnhide>
  {
  }

  public abstract class batchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatchTransaction.batchID>
  {
  }

  public abstract class pCTranNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransaction.pCTranNumber>
  {
  }

  public abstract class pcCustomerID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransaction.pcCustomerID>
  {
  }

  public abstract class pcPaymentProfileID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransaction.pcPaymentProfileID>
  {
  }

  public abstract class settlementStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransaction.settlementStatus>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCBatchTransaction.invoiceNbr>
  {
  }

  public abstract class submitTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCBatchTransaction.submitTime>
  {
  }

  public abstract class procCenterCardTypeCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransaction.procCenterCardTypeCode>
  {
  }

  public abstract class cardTypeCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransaction.cardTypeCode>
  {
  }

  public abstract class displayCardType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransaction.displayCardType>
  {
  }

  public abstract class accountNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransaction.accountNumber>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CCBatchTransaction.amount>
  {
  }

  public abstract class fixedFee : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CCBatchTransaction.fixedFee>
  {
  }

  public abstract class percentageFee : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CCBatchTransaction.percentageFee>
  {
  }

  public abstract class totalFee : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CCBatchTransaction.totalFee>
  {
  }

  public abstract class feeType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCBatchTransaction.feeType>
  {
  }

  public abstract class transactionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatchTransaction.transactionID>
  {
  }

  public abstract class originalStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransaction.originalStatus>
  {
  }

  public abstract class currentStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransaction.currentStatus>
  {
  }

  public abstract class processingStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransaction.processingStatus>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCBatchTransaction.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCBatchTransaction.refNbr>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCBatchTransaction.createdDateTime>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CCBatchTransaction.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransaction.createdByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCBatchTransaction.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CCBatchTransaction.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchTransaction.lastModifiedByScreenID>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CCBatchTransaction.tstamp>
  {
  }

  public abstract class noteid : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CCBatchTransaction.noteid>
  {
  }
}
