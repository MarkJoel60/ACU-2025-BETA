// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ExternalTransaction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.CA;
using PX.Objects.CC;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("External Transaction")]
public class ExternalTransaction : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IExternalTransaction
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  [PXDBIdentity(IsKey = true)]
  [PXUIField]
  [PXSelector(typeof (Search<ExternalTransaction.transactionID>), new Type[] {typeof (ExternalTransaction.transactionID), typeof (ExternalTransaction.tranNumber), typeof (ExternalTransaction.authNumber), typeof (ExternalTransaction.amount), typeof (ExternalTransaction.lastActivityDate), typeof (ExternalTransaction.procStatus), typeof (ExternalTransaction.docType), typeof (ExternalTransaction.refNbr)})]
  public virtual int? TransactionID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  [PXSelector(typeof (Search<CustomerPaymentMethod.pMInstanceID, Where<CustomerPaymentMethod.isActive, Equal<boolTrue>>>), DescriptionField = typeof (CustomerPaymentMethod.descr), ValidateValue = false)]
  public virtual int? PMInstanceID { get; set; }

  /// <summary>Acumatica specific Payment Link Id.</summary>
  [PXDBInt]
  public virtual int? PayLinkID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Proc. Center ID")]
  public virtual 
  #nullable disable
  string ProcessingCenterID { get; set; }

  /// <summary>Terminal ID</summary>
  [PXDBString(36, IsUnicode = true)]
  [PXUIField(DisplayName = "Terminal ID")]
  public virtual string TerminalID { get; set; }

  [PXDBString(3)]
  [PXUIField]
  [ARDocType.List]
  public virtual string DocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<ARRegister.refNbr, Where<ARRegister.docType, Equal<Optional<ExternalTransaction.docType>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBString(3)]
  [PXUIField(DisplayName = "Orig. Doc. Type")]
  [PXSelector(typeof (Search4<SOOrderType.orderType, Aggregate<GroupBy<SOOrderType.orderType>>>), DescriptionField = typeof (SOOrderType.descr))]
  public virtual string OrigDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Orig. Doc. Ref. Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Optional<ExternalTransaction.origDocType>>>>))]
  public virtual string OrigRefNbr { get; set; }

  [PXDBString(3)]
  [ARDocType.List]
  public virtual string VoidDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string VoidRefNbr { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  public virtual string TranNumber { get; set; }

  /// <summary>
  /// Transaction identifier assigned by an external service other than the processing center.
  /// </summary>
  [PXDBString(50, IsUnicode = true)]
  public virtual string TranApiNumber { get; set; }

  /// <summary>
  /// Payment identifier assigned by an external ecommerce system.
  /// </summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Commerce Tran. ID", FieldClass = "CommerceIntegration")]
  public virtual string CommerceTranNumber { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField]
  public virtual string AuthNumber { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? Amount { get; set; }

  /// <summary>Type of a card associated with the document.</summary>
  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "Card/Account Type", Enabled = false)]
  [PX.Objects.AR.CardType.List]
  public virtual string CardType { get; set; }

  /// <summary>
  /// Original card type value received from the processing center.
  /// </summary>
  [PXDBString(25, IsFixed = true)]
  [PXUIField]
  public virtual string ProcCenterCardTypeCode { get; set; }

  [PXDBString(3, IsFixed = true, DatabaseFieldName = "ProcessingStatus")]
  [ExtTransactionProcStatusCode.List]
  [PXUIField]
  public virtual string ProcStatus { get; set; }

  [PXDBDate(PreserveTime = true, DisplayMask = "d")]
  [PXUIField]
  public virtual DateTime? LastActivityDate { get; set; }

  [PXDBString(1, IsFixed = true)]
  public virtual string Direction { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Load Payment Profile")]
  public virtual bool? SaveProfile { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Validation Is Required")]
  public virtual bool? NeedSync { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [CCSyncStatusCode.List]
  [PXUIField(DisplayName = "Validation Status")]
  public virtual string SyncStatus { get; set; }

  [PXDBString(1024 /*0x0400*/, IsUnicode = true)]
  public virtual string SyncMessage { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Ext. Profile ID")]
  public virtual string ExtProfileId { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Completed")]
  public virtual bool? Completed { get; set; }

  [PXDBInt]
  public virtual int? ParentTranID { get; set; }

  [PXDBDate(PreserveTime = true, DisplayMask = "d")]
  [PXUIField(DisplayName = "Expiration Date")]
  public virtual DateTime? ExpirationDate { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXDefault("NOV")]
  [CVVVerificationStatusCode.List]
  [PXUIField(DisplayName = "CVV Verification")]
  public virtual string CVVVerification { get; set; }

  [PXDBDateAndTime]
  public virtual DateTime? FundHoldExpDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Settled")]
  public virtual bool? Settled { get; set; }

  /// <summary>Processing status of Level 3 Data.</summary>
  [PXDBString(3, IsFixed = true, DatabaseFieldName = "L3Status")]
  [ExtTransactionL3StatusCode.List]
  [PXDefault(typeof (ExtTransactionL3StatusCode.notApplicable))]
  [PXUIField]
  public virtual string L3Status { get; set; }

  /// <summary>Processing error text of Level 3 Data.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true, DatabaseFieldName = "L3Error")]
  [PXUIField(Visible = false, DisplayName = "Error Description")]
  public virtual string L3Error { get; set; }

  /// <summary>The last digits of card.</summary>
  [PXDBString(4)]
  [PXUIField(DisplayName = "Last Digits")]
  public virtual string LastDigits { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
  /// </value>
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public class PK : PrimaryKeyOf<ExternalTransaction>.By<ExternalTransaction.transactionID>
  {
    public static ExternalTransaction Find(
      PXGraph graph,
      int? transactionID,
      PKFindOptions options = 0)
    {
      return (ExternalTransaction) PrimaryKeyOf<ExternalTransaction>.By<ExternalTransaction.transactionID>.FindBy(graph, (object) transactionID, options);
    }
  }

  public static class FK
  {
    public class CustomerPaymentMethod : 
      PrimaryKeyOf<CustomerPaymentMethod>.By<CustomerPaymentMethod.pMInstanceID>.ForeignKeyOf<ExternalTransaction>.By<ExternalTransaction.pMInstanceID>
    {
    }

    public class ARPayment : 
      PrimaryKeyOf<ARPayment>.By<ARPayment.docType, ARPayment.refNbr>.ForeignKeyOf<ExternalTransaction>.By<ExternalTransaction.docType, ExternalTransaction.refNbr>
    {
    }

    public class ProcessingCenter : 
      PrimaryKeyOf<CCProcessingCenter>.By<CCProcessingCenter.processingCenterID>.ForeignKeyOf<ExternalTransaction>.By<ExternalTransaction.processingCenterID>
    {
    }

    public class ParentExternalTransaction : 
      PrimaryKeyOf<ExternalTransaction>.By<ExternalTransaction.transactionID>.ForeignKeyOf<ExternalTransaction>.By<ExternalTransaction.parentTranID>
    {
    }

    public class PayLinkID : 
      PrimaryKeyOf<CCPayLink>.By<CCPayLink.payLinkID>.ForeignKeyOf<ExternalTransaction>.By<ExternalTransaction.payLinkID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ExternalTransaction.selected>
  {
  }

  public abstract class transactionID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ExternalTransaction.transactionID>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ExternalTransaction.pMInstanceID>
  {
  }

  public abstract class payLinkID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ExternalTransaction.payLinkID>
  {
  }

  public abstract class processingCenterID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.processingCenterID>
  {
  }

  public abstract class terminalID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.terminalID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExternalTransaction.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExternalTransaction.refNbr>
  {
  }

  public abstract class origDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.origDocType>
  {
  }

  public abstract class origRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.origRefNbr>
  {
  }

  public abstract class voidDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.voidDocType>
  {
  }

  public abstract class voidRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.voidRefNbr>
  {
  }

  public abstract class tranNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.tranNumber>
  {
  }

  public abstract class tranApiNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.tranApiNumber>
  {
  }

  public abstract class commerceTranNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.commerceTranNumber>
  {
  }

  public abstract class authNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.authNumber>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ExternalTransaction.amount>
  {
  }

  public abstract class cardType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExternalTransaction.cardType>
  {
  }

  public abstract class procCenterCardTypeCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.procCenterCardTypeCode>
  {
  }

  public abstract class procStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.procStatus>
  {
  }

  public abstract class lastActivityDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ExternalTransaction.lastActivityDate>
  {
  }

  public abstract class direction : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExternalTransaction.direction>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ExternalTransaction.active>
  {
  }

  public abstract class saveProfile : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ExternalTransaction.saveProfile>
  {
  }

  public abstract class needSync : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ExternalTransaction.needSync>
  {
  }

  public abstract class syncStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.syncStatus>
  {
  }

  public abstract class syncMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.syncMessage>
  {
  }

  public abstract class extProfileId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.extProfileId>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ExternalTransaction.completed>
  {
  }

  public abstract class parentTranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ExternalTransaction.parentTranID>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ExternalTransaction.expirationDate>
  {
  }

  public abstract class cVVVerification : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.cVVVerification>
  {
  }

  public abstract class fundHoldExpDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ExternalTransaction.fundHoldExpDate>
  {
  }

  public abstract class settled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ExternalTransaction.settled>
  {
  }

  public abstract class l3Status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExternalTransaction.l3Status>
  {
  }

  public abstract class l3Error : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExternalTransaction.l3Error>
  {
  }

  public abstract class lastDigits : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExternalTransaction.lastDigits>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ExternalTransaction.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ExternalTransaction.noteID>
  {
  }

  public static class TransactionDirection
  {
    public const string Debet = "D";
    public const string Credit = "C";

    public class debetTransactionDirection : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ExternalTransaction.TransactionDirection.debetTransactionDirection>
    {
      public debetTransactionDirection()
        : base("D")
      {
      }
    }
  }
}
