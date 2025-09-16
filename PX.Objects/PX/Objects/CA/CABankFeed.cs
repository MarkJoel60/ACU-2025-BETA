// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeed
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// The class defines the method for loading bank transactions into the system.
/// </summary>
[PXCacheName("Bank Feed")]
[PXPrimaryGraph(typeof (CABankFeedMaint))]
public class CABankFeed : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  [PXDBDefault]
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Bank Feed ID")]
  [PXSelector(typeof (Search<CABankFeed.bankFeedID>))]
  public virtual 
  #nullable disable
  string BankFeedID { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public int? OrganizationID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("D")]
  [CABankFeedStatus.List]
  [PXUIField(DisplayName = "Status")]
  public virtual string Status { get; set; }

  [PXDBString(1, IsFixed = true)]
  [CABankFeedRetrievalStatus.List]
  [PXUIField(DisplayName = "Retrieval Status")]
  public virtual string RetrievalStatus { get; set; }

  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "Retrieval Date")]
  public DateTime? RetrievalDate { get; set; }

  [PXDBString]
  [PXDefault]
  [CABankFeedType.List]
  [PXUIField(DisplayName = "Bank Feed Type")]
  public virtual string Type { get; set; }

  [PXRSACryptString(IsUnicode = true, IsViewDecrypted = true)]
  [PXUIField(Enabled = false)]
  public virtual string AccessToken { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Item ID/Member ID", Enabled = false)]
  public virtual string ExternalItemID { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "User ID", Enabled = false)]
  public virtual string ExternalUserID { get; set; }

  [PXDBString(150, IsUnicode = true)]
  [PXUIField(DisplayName = "Financial Institution", Enabled = false)]
  public virtual string Institution { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "", Enabled = false)]
  public virtual string InstitutionID { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Descr { get; set; }

  [PXDefault(false)]
  [PXDBDefault]
  [PXDBBool]
  [PXUIField(DisplayName = "Create Expense Receipts")]
  public virtual bool? CreateExpenseReceipt { get; set; }

  [PXDefault(false)]
  [PXDBDefault]
  [PXDBBool]
  [PXUIField(DisplayName = "Create Expense Receipts for Pending Transactions")]
  public virtual bool? CreateReceiptForPendingTran { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the bank feed works in the multiple mapping mode.
  /// </summary>
  [PXDefault(false)]
  [PXDBDefault]
  [PXDBBool]
  [PXUIField(DisplayName = "Map Multiple Bank Accounts to One Cash Account")]
  public virtual bool? MultipleMapping { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.expenseItem>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>>>>>), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  [PXUIField(DisplayName = "Default Expense Item", Enabled = false)]
  public virtual int? DefaultExpenseItemID { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Import Start Date")]
  public virtual DateTime? ImportStartDate { get; set; }

  [PXDBString(250, IsUnicode = true)]
  [PXUIField(DisplayName = "Error message")]
  public virtual string ErrorMessage { get; set; }

  /// <summary>
  /// Returns <c>true</c> when Bank Feed is using a sandbox
  /// </summary>
  [PXBool]
  public virtual bool? IsTestFeed
  {
    get => new bool?(this.Type == "T");
    set
    {
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Statement Import Source", Enabled = false, Visible = false)]
  public virtual string StatementImportSource { get; set; }

  /// <summary>Defines the format of the amount in files.</summary>
  [PXDBString]
  [PXDefault("S")]
  [CABankFeedFileAmountFormat.List]
  [PXUIField(DisplayName = "Amount Format")]
  public virtual string FileAmountFormat { get; set; }

  /// <summary>
  /// Defines the label that indicates that the transaction belongs to the Disbursement type.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Disbursement Property")]
  public virtual string DebitLabel { get; set; }

  /// <summary>
  /// Defines the label that indicates that the transaction belongs to the Receipt type.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Receipt Property")]
  public virtual string CreditLabel { get; set; }

  /// <summary>Defines the folder path to the shared folder.</summary>
  [PXDBString(250, IsUnicode = true)]
  [PXUIField(DisplayName = "URL")]
  public virtual string FolderPath { get; set; }

  /// <summary>
  /// Defines the file format that is stored in the shared folder.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("C")]
  [PXUIField(DisplayName = "File Format")]
  [CABankFeedFileFormat.List]
  public virtual string FileFormat { get; set; }

  /// <summary>Folder login to access the shared folder.</summary>
  [PXDBString(80 /*0x50*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Login")]
  public virtual string FolderLogin { get; set; }

  /// <summary>Folder password to access the shared folder.</summary>
  [PXRSACryptString(IsUnicode = true)]
  [PXUIField(DisplayName = "Password")]
  public virtual string FolderPassword { get; set; }

  /// <summary>
  /// Name of the certificate that is used to connect to the SFTP server.
  /// </summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "SSH Private Key")]
  [PXSelector(typeof (Certificate.name))]
  public virtual string SshCertificateName { get; set; }

  /// <summary>
  /// Defines the provider that is used to read lines in files.
  /// </summary>
  [PXDBGuid(false)]
  [PXForeignReference(typeof (Field<CABankFeed.providerID>.IsRelatedTo<SYProvider.providerID>))]
  [PXSelector(typeof (Search<SYProvider.providerID>), SubstituteKey = typeof (SYProvider.name))]
  [PXUIField(DisplayName = "Data Provider", Enabled = false)]
  public virtual Guid? ProviderID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXInt]
  [PXDBScalar(typeof (Search4<CABankFeedDetail.lineNbr, Where<CABankFeedDetail.bankFeedID, Equal<CABankFeed.bankFeedID>>, Aggregate<Count<CABankFeedDetail.lineNbr>>>))]
  [PXUIField(DisplayName = "Accounts")]
  public virtual int? AccountQty { get; set; }

  [PXInt]
  [PXDBScalar(typeof (Search4<CABankFeedDetail.lineNbr, Where<CABankFeedDetail.bankFeedID, Equal<CABankFeed.bankFeedID>, And<CABankFeedDetail.cashAccountID, IsNull>>, Aggregate<Count<CABankFeedDetail.lineNbr>>>))]
  [PXUIField(DisplayName = "Unmatched Accounts")]
  public virtual int? UnmatchedAccountQty { get; set; }

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

  [PXDBTimestamp]
  [PXUIField(DisplayName = "Tstamp")]
  public virtual byte[] Tstamp { get; set; }

  public class PK : PrimaryKeyOf<CABankFeed>.By<CABankFeed.bankFeedID>
  {
    public static CABankFeed Find(PXGraph graph, string bankFeedID, PKFindOptions options = 0)
    {
      return (CABankFeed) PrimaryKeyOf<CABankFeed>.By<CABankFeed.bankFeedID>.FindBy(graph, (object) bankFeedID, options);
    }
  }

  public static class FK
  {
    public class ExternalUserID : 
      PrimaryKeyOf<CABankFeedUser>.By<CABankFeedUser.externalUserID>.ForeignKeyOf<CABankFeedUser>.By<CABankFeed.externalUserID>
    {
    }

    public class SshCertificateName : 
      PrimaryKeyOf<Certificate>.By<Certificate.name>.ForeignKeyOf<CABankFeed>.By<CABankFeed.sshCertificateName>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankFeed.selected>
  {
  }

  public abstract class bankFeedID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeed.bankFeedID>
  {
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankFeed.organizationID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeed.status>
  {
  }

  public abstract class retrievalStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeed.retrievalStatus>
  {
  }

  public abstract class retrievalDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeed.retrievalDate>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeed.type>
  {
  }

  public abstract class accessToken : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeed.accessToken>
  {
  }

  public abstract class externalItemID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeed.externalItemID>
  {
  }

  public abstract class externalUserID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankFeed.externalUserID>
  {
  }

  public abstract class institution : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeed.institution>
  {
  }

  public abstract class institutionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeed.institutionID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeed.descr>
  {
  }

  public abstract class createExpenseReceipt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeed.createExpenseReceipt>
  {
  }

  public abstract class createReceiptForPendingTran : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeed.createReceiptForPendingTran>
  {
  }

  public abstract class multipleMapping : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankFeed.multipleMapping>
  {
  }

  public abstract class defaultExpenseItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankFeed.defaultExpenseItemID>
  {
  }

  public abstract class importStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeed.importStartDate>
  {
  }

  public abstract class errorMessage : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeed.errorMessage>
  {
  }

  public abstract class isTestFeed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankFeed.isTestFeed>
  {
  }

  public abstract class statementImportSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeed.statementImportSource>
  {
  }

  public abstract class fileAmountFormat : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeed.fileAmountFormat>
  {
  }

  public abstract class debitLabel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeed.debitLabel>
  {
  }

  public abstract class creditLabel : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeed.creditLabel>
  {
  }

  public abstract class folderPath : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeed.folderPath>
  {
  }

  public abstract class fileFormat : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeed.fileFormat>
  {
  }

  public abstract class folderLogin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeed.folderLogin>
  {
  }

  public abstract class folderPassword : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankFeed.folderPassword>
  {
  }

  public abstract class sshCertificateName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeed.sshCertificateName>
  {
  }

  public abstract class providerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankFeed.providerID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankFeed.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeed.createdByScreenID>
  {
  }

  public abstract class accountQty : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankFeed.accountQty>
  {
  }

  public abstract class unmatchedAccountQty : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankFeed.unmatchedAccountQty>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeed.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankFeed.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeed.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeed.lastModifiedDateTime>
  {
  }

  public abstract class noteid : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankFeed.noteid>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CABankFeed.tstamp>
  {
  }
}
