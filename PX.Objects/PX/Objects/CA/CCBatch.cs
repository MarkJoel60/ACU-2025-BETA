// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCBatch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("CCBatch")]
[Serializable]
public class CCBatch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  [PXDBIdentity(IsKey = true)]
  [PXDefault]
  [PXSelector(typeof (Search<CCBatch.batchID>))]
  [PXUIField]
  public virtual int? BatchID { get; set; }

  [PXDBString(3, IsFixed = true)]
  [CCBatchStatusCode.List]
  [PXUIField(DisplayName = "Status")]
  public virtual 
  #nullable disable
  string Status { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Proc. Center ID")]
  public virtual string ProcessingCenterID { get; set; }

  [PXDBScalar(typeof (Search2<CashAccount.curyID, InnerJoin<CCProcessingCenter, On<CCProcessingCenter.cashAccountID, Equal<CashAccount.cashAccountID>>>, Where<CCProcessingCenter.processingCenterID, Equal<CCBatch.processingCenterID>>>))]
  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID { get; set; }

  [PXDBString(50, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Ext. Batch ID")]
  public virtual string ExtBatchID { get; set; }

  [PXDBDate(PreserveTime = true, UseTimeZone = false, DisplayMask = "G", InputMask = "G")]
  [PXUIField(DisplayName = "Settlement Time UTC")]
  public virtual DateTime? SettlementTimeUTC { get; set; }

  [PXDate(InputMask = "G", DisplayMask = "G")]
  [PXUIField(DisplayName = "Settlement Time")]
  public virtual DateTime? SettlementTime
  {
    [PXDependsOnFields(new Type[] {typeof (CCBatch.settlementTimeUTC)})] get
    {
      return !this.SettlementTimeUTC.HasValue ? new DateTime?() : new DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(this.SettlementTimeUTC.Value, LocaleInfo.GetTimeZone()));
    }
  }

  [PXDBString(3, IsFixed = true)]
  [CCBatchSettlementState.List]
  [PXUIField(DisplayName = "Settlement State")]
  public virtual string SettlementState { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Processed Count")]
  public virtual int? ProcessedCount { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Missing Transaction Count")]
  public virtual int? MissingCount { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Hidden Count")]
  public virtual int? HiddenCount { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Excluded from Deposit Count")]
  public virtual int? ExcludedCount { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Transaction Count")]
  public virtual int? TransactionCount { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Imported Transaction Count")]
  public virtual int? ImportedTransactionCount { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Unprocessed Transaction Count")]
  public virtual int? UnprocessedCount { get; set; }

  [PXDBCury(typeof (CCBatch.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Settled Amount")]
  public virtual Decimal? SettledAmount { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Settled Count")]
  public virtual int? SettledCount { get; set; }

  [PXDBCury(typeof (CCBatch.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Refund Amount")]
  public virtual Decimal? RefundAmount { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Refund Count")]
  public virtual int? RefundCount { get; set; }

  /// <summary>The total amount of rejected transactions.</summary>
  [PXDBCury(typeof (CCBatch.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Rejected Amount")]
  public virtual Decimal? RejectedAmount { get; set; }

  /// <summary>The total count of rejected transactions.</summary>
  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Rejected Count")]
  public virtual int? RejectedCount { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Void Count")]
  public virtual int? VoidCount { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Decline Count")]
  public virtual int? DeclineCount { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Error Count")]
  public virtual int? ErrorCount { get; set; }

  [PXDBString(3, IsFixed = true)]
  public virtual string DepositType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<CADeposit.refNbr, Where<CADeposit.tranType, Equal<Current<CCBatch.depositType>>>>))]
  public virtual string DepositNbr { get; set; }

  /// <summary>
  /// Skip automatic creation of a deposit when the Status changes to "Processed"
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? SkipDepositAutoCreation { get; set; }

  /// <summary>Batch type (uses values from CCBatchType)</summary>
  [PXDBString(3, IsFixed = true)]
  [CCBatchType.List]
  public virtual string BatchType { get; set; }

  [PXString]
  public virtual string Description
  {
    [PXDependsOnFields(new Type[] {typeof (CCBatch.processingCenterID), typeof (CCBatch.settlementTimeUTC)})] get
    {
      return $"{this.ProcessingCenterID}: {this.SettlementTime.ToString()}";
    }
    set
    {
    }
  }

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

  public class PK : PrimaryKeyOf<CCBatch>.By<CCBatch.batchID>
  {
    public static CCBatch Find(PXGraph graph, int? batchID, PKFindOptions options = 0)
    {
      return (CCBatch) PrimaryKeyOf<CCBatch>.By<CCBatch.batchID>.FindBy(graph, (object) batchID, options);
    }
  }

  public static class FK
  {
    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CCBatch>.By<CCBatch.curyID>
    {
    }

    public class CADeposit : 
      PrimaryKeyOf<CADeposit>.By<CADeposit.tranType, CADeposit.refNbr>.ForeignKeyOf<CCBatch>.By<CCBatch.depositType, CCBatch.depositNbr>
    {
    }

    public class CCProcessingCenter : 
      PrimaryKeyOf<CCProcessingCenter>.By<CCProcessingCenter.processingCenterID>.ForeignKeyOf<CCBatch>.By<CCBatch.processingCenterID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CCBatch.selected>
  {
  }

  public abstract class batchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatch.batchID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCBatch.status>
  {
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatch.processingCenterID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCBatch.curyID>
  {
  }

  public abstract class extBatchID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCBatch.extBatchID>
  {
  }

  public abstract class settlementTimeUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCBatch.settlementTimeUTC>
  {
  }

  public abstract class settlementTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCBatch.settlementTime>
  {
  }

  public abstract class settlementState : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCBatch.settlementState>
  {
  }

  public abstract class processedCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatch.processedCount>
  {
  }

  public abstract class missingCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatch.missingCount>
  {
  }

  public abstract class hiddenCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatch.hiddenCount>
  {
  }

  public abstract class excludedCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatch.excludedCount>
  {
  }

  public abstract class transactionCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatch.transactionCount>
  {
  }

  public abstract class importedTransactionCount : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CCBatch.importedTransactionCount>
  {
  }

  public abstract class unprocessedCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatch.unprocessedCount>
  {
  }

  public abstract class settledAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CCBatch.settledAmount>
  {
  }

  public abstract class settledCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatch.settledCount>
  {
  }

  public abstract class refundAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CCBatch.refundAmount>
  {
  }

  public abstract class refundCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatch.refundCount>
  {
  }

  public abstract class rejectedAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CCBatch.rejectedAmount>
  {
  }

  public abstract class rejectedCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatch.rejectedCount>
  {
  }

  public abstract class voidCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatch.voidCount>
  {
  }

  public abstract class declineCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatch.declineCount>
  {
  }

  public abstract class errorCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCBatch.errorCount>
  {
  }

  public abstract class depositType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCBatch.depositType>
  {
  }

  public abstract class depositNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCBatch.depositNbr>
  {
  }

  public abstract class skipDepositAutoCreation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatch.skipDepositAutoCreation>
  {
  }

  public abstract class batchType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCBatch.batchType>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCBatch.createdDateTime>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CCBatch.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatch.createdByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCBatch.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CCBatch.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCBatch.lastModifiedByScreenID>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CCBatch.tstamp>
  {
  }

  public abstract class noteid : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CCBatch.noteid>
  {
  }
}
