// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// Contains the information about the feed account associated with Bank Feed.
/// </summary>
[PXCacheName("Bank Feed Detail")]
public class CABankFeedDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CABankFeed.bankFeedID))]
  [PXParent(typeof (CABankFeedDetail.FK.BankFeed))]
  public virtual 
  #nullable disable
  string BankFeedID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (CABankFeed))]
  [PXUIField(Visible = false)]
  public virtual int? LineNbr { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Account ID", Enabled = false)]
  public virtual string AccountID { get; set; }

  [PXDBString(250)]
  [PXUIField(DisplayName = "Account Name", Enabled = false)]
  public virtual string AccountName { get; set; }

  [PXDBString(50)]
  [PXUIField(DisplayName = "Account Mask", Enabled = false)]
  public virtual string AccountMask { get; set; }

  [PXDBString(100)]
  [PXUIField(DisplayName = "Account Type", Enabled = false)]
  public virtual string AccountType { get; set; }

  [PXDBString(100)]
  [PXUIField(DisplayName = "Account Subtype", Enabled = false)]
  public virtual string AccountSubType { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXSelector(typeof (Search<CurrencyList.curyID, Where<CurrencyList.isActive, Equal<True>, And<CurrencyList.isFinancial, Equal<True>>>>), ValidateValue = false)]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  public virtual string Currency { get; set; }

  [PXDBString(60)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Descr { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<CashAccount.cashAccountID>), SubstituteKey = typeof (CashAccount.cashAccountCD), DescriptionField = typeof (CashAccount.descr))]
  [PXUIField(DisplayName = "Cash Account")]
  public virtual int? CashAccountID { get; set; }

  [PXDefault("M")]
  [PXDBString]
  [CABankFeedStatementPeriod.List]
  [PXUIField(DisplayName = "Statement Period")]
  public virtual string StatementPeriod { get; set; }

  [PXDefault(1)]
  [PXDBInt]
  [CABankFeedStatementStartDay(typeof (CABankFeedDetail.statementPeriod))]
  [PXUIField(DisplayName = "Statement Start Day")]
  public virtual int? StatementStartDay { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Import Transactions From", Enabled = false)]
  public virtual DateTime? ImportStartDate { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the import start date was overridden by a user.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? OverrideDate { get; set; }

  /// <summary>
  /// The manual import start date to retrieve new transactions for the bank feed account.
  /// </summary>
  [PXDBDate]
  public virtual DateTime? ManualImportDate { get; set; }

  [PXDBString(1, IsFixed = true)]
  [CABankFeedRetrievalStatus.List]
  [PXUIField(DisplayName = "Retrieval Status", Enabled = false)]
  public virtual string RetrievalStatus { get; set; }

  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "Retrieval Date", Enabled = false)]
  public DateTime? RetrievalDate { get; set; }

  [PXDBString(250, IsUnicode = true)]
  [PXUIField(DisplayName = "Error message", Enabled = false)]
  public virtual string ErrorMessage { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that this Cash Account has been hidden from details on the Bank Feed (CA205500) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Hidden", Enabled = false)]
  public virtual bool? Hidden { get; set; }

  /// <summary>
  /// Defines the current file to process or the last processed one for the feed account.
  /// </summary>
  [PXDBGuid(false)]
  public virtual Guid? FileID { get; set; }

  /// <summary>
  /// Defines the current file revision to process or the last processed one for the feed account.
  /// </summary>
  [PXDBInt]
  public virtual int? FileRevisionID { get; set; }

  /// <summary>
  /// Defines the current file date and time to process or the last processed one for the feed account.
  /// </summary>
  [PXDBDate(PreserveTime = true)]
  public DateTime? FileDateTime { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "Tstamp")]
  public virtual byte[] Tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created Date Time")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified Date Time")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXNote]
  [PXUIField(DisplayName = "Noteid")]
  public virtual Guid? Noteid { get; set; }

  public class PK : 
    PrimaryKeyOf<CABankFeedDetail>.By<CABankFeedDetail.bankFeedID, CABankFeedDetail.lineNbr>
  {
    public static CABankFeedDetail Find(
      PXGraph graph,
      string bankFeedID,
      int? bankFeedDetailID,
      PKFindOptions options = 0)
    {
      return (CABankFeedDetail) PrimaryKeyOf<CABankFeedDetail>.By<CABankFeedDetail.bankFeedID, CABankFeedDetail.lineNbr>.FindBy(graph, (object) bankFeedID, (object) bankFeedDetailID, options);
    }
  }

  public static class FK
  {
    public class BankFeed : 
      PrimaryKeyOf<CABankFeed>.By<CABankFeed.bankFeedID>.ForeignKeyOf<CABankFeedDetail>.By<CABankFeedDetail.bankFeedID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CABankFeedDetail>.By<CABankFeedDetail.cashAccountID>
    {
    }
  }

  public abstract class bankFeedID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedDetail.bankFeedID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankFeedDetail.lineNbr>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedDetail.accountID>
  {
  }

  public abstract class accountName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedDetail.accountName>
  {
  }

  public abstract class accountMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedDetail.accountMask>
  {
  }

  public abstract class accountType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedDetail.accountType>
  {
  }

  public abstract class accountSubType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedDetail.accountSubType>
  {
  }

  public abstract class currency : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedDetail.currency>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeedDetail.descr>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankFeedDetail.cashAccountID>
  {
  }

  public abstract class statementPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedDetail.statementPeriod>
  {
  }

  public abstract class statementStartDay : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankFeedDetail.statementStartDay>
  {
  }

  public abstract class importStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeedDetail.importStartDate>
  {
  }

  public abstract class overrideDate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankFeedDetail.overrideDate>
  {
  }

  public abstract class manualImportDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeedDetail.manualImportDate>
  {
  }

  public abstract class retrievalStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedDetail.retrievalStatus>
  {
  }

  public abstract class retrievalDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeedDetail.retrievalDate>
  {
  }

  public abstract class errorMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedDetail.errorMessage>
  {
  }

  public abstract class hidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankFeedDetail.hidden>
  {
  }

  public abstract class fileID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankFeedDetail.fileID>
  {
  }

  public abstract class fileRevisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankFeedDetail.fileRevisionID>
  {
  }

  public abstract class fileDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeedDetail.fileDateTime>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CABankFeedDetail.tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankFeedDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeedDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CABankFeedDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeedDetail.lastModifiedDateTime>
  {
  }

  public abstract class noteid : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankFeedDetail.noteid>
  {
  }
}
