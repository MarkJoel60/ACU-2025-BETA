// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCBatchStatistics
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
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("CCBatchStatistics")]
[Serializable]
public class CCBatchStatistics : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Batch ID")]
  [PXDBDefault(typeof (CCBatch.batchID))]
  [PXParent(typeof (Select<CCBatch, Where<CCBatch.batchID, Equal<Current<CCBatchStatistics.batchID>>>>))]
  [PXUnboundFormula(typeof (Add<CCBatchStatistics.settledCount, Add<CCBatchStatistics.refundCount, Add<CCBatchStatistics.voidCount, Add<CCBatchStatistics.declineCount, Add<CCBatchStatistics.rejectedCount, CCBatchStatistics.errorCount>>>>>), typeof (SumCalc<CCBatch.transactionCount>))]
  public virtual int? BatchID { get; set; }

  /// <summary>
  /// Original card type value received from the processing center.
  /// </summary>
  [PXDBString(25, IsUnicode = true, InputMask = "", IsKey = true)]
  [PXUIField(DisplayName = "Proc. Center Card Type", Enabled = false)]
  public virtual 
  #nullable disable
  string ProcCenterCardTypeCode { get; set; }

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
  [PXFormula(typeof (Switch<Case<Where<CCBatchStatistics.cardTypeCode, IsNull>, CCBatchStatistics.procCenterCardTypeCode, Case<Where<CCBatchStatistics.cardTypeCode, Equal<CardType.other>, And<CCBatchStatistics.procCenterCardTypeCode, IsNotNull>>, BqlOperand<Concat<TypeArrayOf<IBqlOperand>.FilledWith<ListLabelOf<CCBatchStatistics.cardTypeCode>.Evaluator, Colon>>, IBqlString>.Concat<CCBatchStatistics.procCenterCardTypeCode>>>, ListLabelOf<CCBatchStatistics.cardTypeCode>>))]
  public virtual string DisplayCardType { get; set; }

  [PXDBCury(typeof (CCBatch.curyID))]
  [PXUIField(DisplayName = "Settled Amount")]
  [PXFormula(null, typeof (SumCalc<CCBatch.settledAmount>))]
  public virtual Decimal? SettledAmount { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Settled Count")]
  [PXFormula(null, typeof (SumCalc<CCBatch.settledCount>))]
  public virtual int? SettledCount { get; set; }

  [PXDBCury(typeof (CCBatch.curyID))]
  [PXUIField(DisplayName = "Refund Amount")]
  [PXFormula(null, typeof (SumCalc<CCBatch.refundAmount>))]
  public virtual Decimal? RefundAmount { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Refund Count")]
  [PXFormula(null, typeof (SumCalc<CCBatch.refundCount>))]
  public virtual int? RefundCount { get; set; }

  /// <summary>The total amount of rejected transactions.</summary>
  [PXDBCury(typeof (CCBatch.curyID))]
  [PXUIField(DisplayName = "Rejected Amount")]
  [PXFormula(null, typeof (SumCalc<CCBatch.rejectedAmount>))]
  public virtual Decimal? RejectedAmount { get; set; }

  /// <summary>The total count of rejected transactions.</summary>
  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Rejected Count")]
  [PXFormula(null, typeof (SumCalc<CCBatch.rejectedCount>))]
  public virtual int? RejectedCount { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Void Count")]
  [PXFormula(null, typeof (SumCalc<CCBatch.voidCount>))]
  public virtual int? VoidCount { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Decline Count")]
  [PXFormula(null, typeof (SumCalc<CCBatch.declineCount>))]
  public virtual int? DeclineCount { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Error Count")]
  [PXFormula(null, typeof (SumCalc<CCBatch.errorCount>))]
  public virtual int? ErrorCount { get; set; }

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
    PrimaryKeyOf<CCBatchStatistics>.By<CCBatchStatistics.batchID, CCBatchStatistics.procCenterCardTypeCode>
  {
    public static CCBatchStatistics Find(
      PXGraph graph,
      int? batchID,
      string cardType,
      PKFindOptions options = 0)
    {
      return (CCBatchStatistics) PrimaryKeyOf<CCBatchStatistics>.By<CCBatchStatistics.batchID, CCBatchStatistics.procCenterCardTypeCode>.FindBy(graph, (object) batchID, (object) cardType, options);
    }
  }

  public static class FK
  {
    public class CCBatch : 
      PrimaryKeyOf<CCBatch>.By<CCBatch.batchID>.ForeignKeyOf<CCBatchStatistics>.By<CCBatchStatistics.batchID>
    {
    }
  }

  public abstract class batchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatchStatistics.batchID>
  {
  }

  public abstract class procCenterCardTypeCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchStatistics.procCenterCardTypeCode>
  {
  }

  public abstract class cardTypeCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchStatistics.cardTypeCode>
  {
  }

  public abstract class displayCardType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchStatistics.displayCardType>
  {
  }

  public abstract class settledAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CCBatchStatistics.settledAmount>
  {
  }

  public abstract class settledCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatchStatistics.settledCount>
  {
  }

  public abstract class refundAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CCBatchStatistics.refundAmount>
  {
  }

  public abstract class refundCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatchStatistics.refundCount>
  {
  }

  public abstract class rejectedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CCBatchStatistics.rejectedAmount>
  {
  }

  public abstract class rejectedCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatchStatistics.rejectedCount>
  {
  }

  public abstract class voidCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatchStatistics.voidCount>
  {
  }

  public abstract class declineCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatchStatistics.declineCount>
  {
  }

  public abstract class errorCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatchStatistics.errorCount>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCBatchStatistics.createdDateTime>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CCBatchStatistics.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchStatistics.createdByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCBatchStatistics.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CCBatchStatistics.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatchStatistics.lastModifiedByScreenID>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CCBatchStatistics.tstamp>
  {
  }

  public abstract class noteid : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CCBatchStatistics.noteid>
  {
  }
}
