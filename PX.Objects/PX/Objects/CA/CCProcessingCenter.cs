// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCProcessingCenter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Webhooks.DAC;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CC;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("Processing Center")]
[PXPrimaryGraph(typeof (CCProcessingCenterMaint))]
[Serializable]
public class CCProcessingCenter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXSelector(typeof (Search<CCProcessingCenter.processingCenterID>))]
  [PXUIField]
  public virtual 
  #nullable disable
  string ProcessingCenterID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault("")]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Name { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [CashAccount]
  [PXDefault]
  public virtual int? CashAccountID { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXDefault]
  [PXCCPluginTypeSelector]
  [DeprecatedProcessing(ChckVal = DeprecatedProcessingAttribute.CheckVal.ProcessingCenterType)]
  [PXUIField(DisplayName = "Payment Plug-In")]
  public virtual string ProcessingTypeName { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Assembly Name")]
  public virtual string ProcessingAssemblyName { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 60)]
  [PXDefault(TypeCode.Int32, "0")]
  [PXUIField]
  public virtual int? OpenTranTimeout { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Direct Input", Visible = false)]
  public virtual bool? AllowDirectInput { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? NeedsExpDateUpdate { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Synchronize Deletion", Visible = false)]
  public virtual bool? SyncronizeDeletion { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Accept Payments from New Cards")]
  public virtual bool? UseAcceptPaymentForm { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Allow Saving Payment Profiles")]
  public virtual bool? AllowSaveProfile { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Unlinked Refunds")]
  public virtual bool? AllowUnlinkedRefund { get; set; }

  /// <summary>Allow Increasing Authorized Amounts</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Increasing Authorized Amounts")]
  public virtual bool? AllowAuthorizedIncrement { get; set; }

  /// <summary>Accept Payments from POS Terminals</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Accept Payments from POS Terminals")]
  public virtual bool? AcceptPOSPayments { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 10)]
  [PXDefault(TypeCode.Int32, "3")]
  [PXUIField]
  public virtual int? SyncRetryAttemptsNo { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 1000)]
  [PXDefault(TypeCode.Int32, "500")]
  [PXUIField]
  public virtual int? SyncRetryDelayMs { get; set; }

  [PXDBInt(MinValue = 1)]
  [PXDefault(10)]
  [PXUIField(DisplayName = "Maximum Credit Cards per Profile", Visible = true)]
  public virtual int? CreditCardLimit { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create Additional Customer Profiles", Visible = true)]
  public virtual bool? CreateAdditionalCustomerProfiles { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Import Settlement Batches")]
  public virtual bool? ImportSettlementBatches { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Import Start Date")]
  public virtual DateTime? ImportStartDate { get; set; }

  [PXDate(InputMask = "G", DisplayMask = "G")]
  [PXUIField(DisplayName = "Last Settlement Date UTC", Enabled = false)]
  [PXDBScalar(typeof (Search<CCBatch.settlementTimeUTC, Where<CCBatch.processingCenterID, Equal<CCProcessingCenter.processingCenterID>>, OrderBy<Desc<CCBatch.settlementTimeUTC>>>))]
  public virtual DateTime? LastSettlementDateUTC { get; set; }

  [PXDate(InputMask = "G", DisplayMask = "G")]
  [PXUIField(DisplayName = "Last Settlement Date", Enabled = false)]
  public virtual DateTime? LastSettlementDate
  {
    [PXDependsOnFields(new Type[] {typeof (CCProcessingCenter.lastSettlementDateUTC)})] get
    {
      return !this.LastSettlementDateUTC.HasValue ? new DateTime?() : new DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(this.LastSettlementDateUTC.Value, LocaleInfo.GetTimeZone()));
    }
  }

  [PXDBInt]
  [PXDefault(3)]
  [PXUIField(DisplayName = "Reauthorization Retry Delay (Hours)")]
  public virtual int? ReauthRetryDelay { get; set; }

  [PXDBInt]
  [PXDefault(3)]
  [PXUIField(DisplayName = "Number of Reauthorization Retries")]
  public virtual int? ReauthRetryNbr { get; set; }

  [PXDefault]
  [CashAccount(typeof (Search<CashAccount.cashAccountID, Where<CashAccount.clearingAccount, NotEqual<boolTrue>, And<CashAccount.curyID, Equal<Current<CashAccount.curyID>>>>>), DisplayName = "Deposit Account")]
  public virtual int? DepositAccountID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Automatically Create Bank Deposits")]
  public virtual bool? AutoCreateBankDeposit { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsExternalAuthorizationOnly { get; set; }

  /// <summary>Allow the Payment Link feature for Processing Center.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Payment Links")]
  public virtual bool? AllowPayLink { get; set; }

  /// <summary>
  /// Indicates that Customers can pay for the payment link partially.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Partial Payment")]
  public virtual bool? AllowPartialPayment { get; set; }

  /// <summary>
  /// Indicates that document details should be attached to the payment link as a file.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Attach Document Details as PDF")]
  public virtual bool? AttachDetailsToPayLink { get; set; }

  /// <summary>
  /// Indicates the document that will be created from the Sales Order Payment link.
  /// </summary>
  [PXDBString(3, IsFixed = true)]
  [PXDefault("PMT")]
  [PayLinkDocumentType.List]
  [PXUIField(DisplayName = "Create from SO Payment Link")]
  public virtual string DocTypeForSOPayLink { get; set; }

  /// <summary>
  /// Acumatica specidic Webhook Id. Id points to a database record with the webhook handler.
  /// </summary>
  [PXDBGuid(false)]
  [PXForeignReference(typeof (Field<CCProcessingCenter.webhookID>.IsRelatedTo<WebHook.webHookID>))]
  [PXSelector(typeof (Search<WebHook.webHookID>))]
  [PXUIField(DisplayName = "Webhook ID")]
  public virtual Guid? WebhookID { get; set; }

  [PXNote(DescriptionField = typeof (CCProcessingCenter.processingCenterID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<CCProcessingCenter>.By<CCProcessingCenter.processingCenterID>
  {
    public static CCProcessingCenter Find(
      PXGraph graph,
      string processingCenterID,
      PKFindOptions options = 0)
    {
      return (CCProcessingCenter) PrimaryKeyOf<CCProcessingCenter>.By<CCProcessingCenter.processingCenterID>.FindBy(graph, (object) processingCenterID, options);
    }
  }

  public static class FK
  {
    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CCProcessingCenter>.By<CCProcessingCenter.cashAccountID>
    {
    }
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenter.processingCenterID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CCProcessingCenter.name>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CCProcessingCenter.isActive>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CCProcessingCenter.cashAccountID>
  {
  }

  public abstract class processingTypeName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenter.processingTypeName>
  {
  }

  public abstract class processingAssemblyName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenter.processingAssemblyName>
  {
  }

  public abstract class openTranTimeout : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CCProcessingCenter.openTranTimeout>
  {
  }

  public abstract class allowDirectInput : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenter.allowDirectInput>
  {
  }

  public abstract class syncronizeDeletion : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenter.syncronizeDeletion>
  {
  }

  public abstract class useAcceptPaymentForm : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenter.useAcceptPaymentForm>
  {
  }

  public abstract class allowSaveProfile : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenter.allowSaveProfile>
  {
  }

  public abstract class allowUnlinkedRefund : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenter.allowUnlinkedRefund>
  {
  }

  public abstract class allowAuthorizedIncrement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenter.allowAuthorizedIncrement>
  {
  }

  public abstract class acceptPOSPayments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenter.acceptPOSPayments>
  {
  }

  public abstract class syncRetryAttemptsNo : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CCProcessingCenter.syncRetryAttemptsNo>
  {
  }

  public abstract class syncRetryDelayMs : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CCProcessingCenter.syncRetryDelayMs>
  {
  }

  public abstract class creditCardLimit : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CCProcessingCenter.creditCardLimit>
  {
  }

  public abstract class createAdditionalCustomerProfiles : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenter.createAdditionalCustomerProfiles>
  {
  }

  public abstract class importSettlementBatches : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenter.importSettlementBatches>
  {
  }

  public abstract class importStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcessingCenter.importStartDate>
  {
  }

  public abstract class lastSettlementDateUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcessingCenter.lastSettlementDateUTC>
  {
  }

  public abstract class lastSettlementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcessingCenter.lastSettlementDate>
  {
  }

  public abstract class reauthRetryDelay : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcessingCenter.reauthRetryDelay>
  {
  }

  public abstract class reauthRetryNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CCProcessingCenter.reauthRetryNbr>
  {
  }

  public abstract class depositAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CCProcessingCenter.depositAccountID>
  {
  }

  public abstract class autoCreateBankDeposit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenter.autoCreateBankDeposit>
  {
  }

  public abstract class isExternalAuthorizationOnly : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenter.isExternalAuthorizationOnly>
  {
  }

  public abstract class allowPayLink : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CCProcessingCenter.allowPayLink>
  {
  }

  public abstract class allowPartialPayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenter.allowPartialPayment>
  {
  }

  public abstract class attachDetailsToPayLink : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CCProcessingCenter.attachDetailsToPayLink>
  {
  }

  public abstract class docTypeForSOPayLink : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenter.docTypeForSOPayLink>
  {
  }

  public abstract class webhookID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CCProcessingCenter.webhookID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CCProcessingCenter.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CCProcessingCenter.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenter.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcessingCenter.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CCProcessingCenter.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CCProcessingCenter.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CCProcessingCenter.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CCProcessingCenter.Tstamp>
  {
  }
}
