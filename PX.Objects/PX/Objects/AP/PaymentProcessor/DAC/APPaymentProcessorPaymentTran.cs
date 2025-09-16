// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.DAC.APPaymentProcessorPaymentTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP.PaymentProcessor.Descriptor;
using PX.PaymentProcessor.Data;
using System;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor.DAC;

/// <summary>AP external payment processor payment history</summary>
[PXCacheName("External Payment Processor Transaction History")]
public class APPaymentProcessorPaymentTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Transaction Number</summary>
  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Record ID", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual int? TranNbr { get; set; }

  /// <summary>External payment processor ID</summary>
  [PXDBString(10, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Processor ID", Visible = false, Enabled = false)]
  public virtual string? ExternalPaymentProcessorID { get; set; }

  /// <summary>Document type</summary>
  [PXDBString(3, IsUnicode = true, IsFixed = true)]
  [PXUIField(DisplayName = "Doc Type", Enabled = false)]
  public virtual string? DocType { get; set; }

  /// <summary>Document reference number</summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Ref Nbr", Enabled = false)]
  public virtual string? RefNbr { get; set; }

  /// <summary>External payment id</summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "Payment ID", Enabled = false)]
  public virtual string? ExternalPaymentID { get; set; }

  /// <summary>External payment status</summary>
  [PXDBString(50)]
  [PaymentStatus.List]
  [PXUIField(DisplayName = "Processing Status", Enabled = false)]
  public virtual string? Status { get; set; }

  /// <summary>State</summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("O")]
  [PaymentTransactionState.List]
  [PXUIField(DisplayName = "Transaction State", Enabled = false)]
  public virtual string? TransactionState { get; set; }

  /// <summary>External user id</summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "User ID", Visible = false, Enabled = false)]
  public virtual string? ExternalUserID { get; set; }

  /// <summary>Process date</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Process Date", Enabled = false)]
  public virtual System.DateTime? ProcessDate { get; set; }

  /// <summary>Funding account id</summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "Funding Account", Visible = false, Enabled = false)]
  public virtual string? FundingAcctID { get; set; }

  /// <summary>Funding account amount</summary>
  [PXDBString(20)]
  [PXUIField(DisplayName = "Disbursement Acct Nbr", Enabled = false)]
  public virtual string? DisbursementAcctNbr { get; set; }

  /// <summary>Funding account currency id</summary>
  [PXDBString(3)]
  [PXUIField(DisplayName = "Funding Currency", Visible = false, Enabled = false)]
  public virtual string? FundingCuryID { get; set; }

  /// <summary>Disbursement type</summary>
  [PXDBString(25)]
  [PXUIField(DisplayName = "Disbursement Method", Enabled = false)]
  public virtual string? DisbursementType { get; set; }

  /// <summary>Disbursement amount</summary>
  [PXDBDecimal]
  [PXUIField(DisplayName = "Disbursement Amount", Enabled = false)]
  public virtual Decimal? DisbursementAmount { get; set; }

  /// <summary>Disbursement currency id</summary>
  [PXDBString(3)]
  [PXUIField(DisplayName = "Disbursement Currency", Visible = false, Enabled = false)]
  public virtual string? DisbursementCuryID { get; set; }

  /// <summary>Disbursement arrive date</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Arrives by Date", Enabled = false)]
  public virtual System.DateTime? DisbursementArriveDate { get; set; }

  /// <summary>External vendor id</summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "Vendor ID", Visible = false, Enabled = false)]
  public virtual string? ExternalVendorID { get; set; }

  /// <summary>Billing type</summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "Billing Type", Visible = false, Enabled = false)]
  public virtual string? BillingType { get; set; }

  /// <summary>Transaction number</summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "Transaction Number", Visible = false, Enabled = false)]
  public virtual string? TransactionNumber { get; set; }

  /// <summary>External updated date time</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Updated Date", Enabled = false)]
  public virtual System.DateTime? ExternalUpdatedDateTime { get; set; }

  /// <summary>External comment</summary>
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Response Comment", Enabled = false)]
  public virtual string? ExternalComment { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string? CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "Tstamp")]
  public virtual byte[]? Tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<APPaymentProcessorPaymentTran>.By<APPaymentProcessorPaymentTran.tranNbr>
  {
    public static APPaymentProcessorPaymentTran Find(
      PXGraph graph,
      int? tranNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APPaymentProcessorPaymentTran>.By<APPaymentProcessorPaymentTran.tranNbr>.FindBy(graph, (object) tranNbr, options);
    }
  }

  public static class FK
  {
    public class APPaymentProcessorPaymentTranKey : 
      PrimaryKeyOf<APExternalPaymentProcessor>.By<APExternalPaymentProcessor.externalPaymentProcessorID>.ForeignKeyOf<APPaymentProcessorPaymentTran>.By<APPaymentProcessorPaymentTran.externalPaymentProcessorID>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<
      #nullable disable
      APRegister>.By<APRegister.docType, APRegister.refNbr>.ForeignKeyOf<
      #nullable enable
      APPaymentProcessorPaymentTran>.By<APPaymentProcessorPaymentTran.docType, APPaymentProcessorPaymentTran.refNbr>
    {
    }
  }

  public abstract class tranNbr : BqlType<IBqlInt, int>.Field<APPaymentProcessorPaymentTran.tranNbr>
  {
  }

  public abstract class externalPaymentProcessorID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.externalPaymentProcessorID>
  {
  }

  public abstract class docType : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.docType>
  {
  }

  public abstract class refNbr : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.refNbr>
  {
  }

  public abstract class externalPaymentID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.externalPaymentID>
  {
  }

  public abstract class status : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.status>
  {
  }

  public abstract class transactionState : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.transactionState>
  {
  }

  public abstract class externalUserID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.externalUserID>
  {
  }

  public abstract class processDate : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APPaymentProcessorPaymentTran.processDate>
  {
  }

  public abstract class fundingAcctID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.fundingAcctID>
  {
  }

  public abstract class disbursementAcctNbr : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.disbursementAcctNbr>
  {
  }

  public abstract class fundingCuryID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.fundingCuryID>
  {
  }

  public abstract class disbursementType : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.disbursementType>
  {
  }

  public abstract class disbursementAmount : 
    BqlType<IBqlDecimal, Decimal>.Field<APPaymentProcessorPaymentTran.disbursementAmount>
  {
  }

  public abstract class disbursementCuryID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.disbursementCuryID>
  {
  }

  public abstract class disbursementArriveDate : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APPaymentProcessorPaymentTran.disbursementArriveDate>
  {
  }

  public abstract class externalVendorID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.externalVendorID>
  {
  }

  public abstract class billingType : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.billingType>
  {
  }

  public abstract class transactionNumber : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.transactionNumber>
  {
  }

  public abstract class externalUpdatedDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APPaymentProcessorPaymentTran.externalUpdatedDateTime>
  {
  }

  public abstract class externalComment : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.externalComment>
  {
  }

  public abstract class createdByID : 
    BqlType<IBqlGuid, Guid>.Field<APPaymentProcessorPaymentTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<IBqlString, string>.Field<APPaymentProcessorPaymentTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APPaymentProcessorPaymentTran.createdDateTime>
  {
  }

  public abstract class tstamp : 
    BqlType<IBqlByteArray, byte[]>.Field<APPaymentProcessorPaymentTran.tstamp>
  {
  }
}
