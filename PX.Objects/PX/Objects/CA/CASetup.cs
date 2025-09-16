// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CASetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// The main properties of cash management preferences and their classes.
/// Cash management preferences are edited on the Cash Management Preferences (CA101000) form
/// (which corresponds to the <see cref="T:PX.Objects.CA.CASetupMaint" /> graph).
/// </summary>
[PXPrimaryGraph(typeof (CASetupMaint))]
[PXCacheName("Cash Management Preferences")]
[Serializable]
public class CASetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The numbering sequence to be used for the identifiers of batches originating in the Cash Management module.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CS.Numbering.NumberingID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("BATCH")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Batch Numbering Sequence")]
  public virtual 
  #nullable disable
  string BatchNumberingID { get; set; }

  /// <summary>
  /// The numbering sequence to be used for the identifiers of cash adjustments or direct cash transactions.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CS.Numbering.NumberingID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("CATRAN")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Transaction Numbering Sequence")]
  public virtual string RegisterNumberingID { get; set; }

  /// <summary>
  /// The numbering sequence to be used for the identifiers of transfers.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CS.Numbering.NumberingID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("CATRANSFER")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Transfer Numbering Sequence")]
  public virtual string TransferNumberingID { get; set; }

  /// <summary>
  /// The numbering sequence to be used for the identifiers of batches of payments.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CS.Numbering.NumberingID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("CABATCH")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Payment Batch Numbering Sequence")]
  public virtual string CABatchNumberingID { get; set; }

  /// <summary>
  /// The numbering sequence to be used for the identifiers of bank statements.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CS.Numbering.NumberingID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault("CABANKSTMT")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Bank Statement Numbering Sequence")]
  public virtual string CAStatementNumberingID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Corporate Card Numbering Sequence")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXDefault("CORPCC")]
  public virtual string CorpCardNumberingID { get; set; }

  /// <summary>
  /// The special multi-currency asset account used (when necessary) as an intermediate account for currency conversions performed during funds transfers.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="T:PX.Objects.GL.Account.accountID" /> field.
  /// </value>
  [PXDefault]
  [PXNonCashAccount(DisplayName = "Cash-In-Transit Account", DescriptionField = typeof (PX.Objects.GL.Account.description), AvoidControlAccounts = true)]
  [PXForeignReference(typeof (CASetup.FK.CashInTransitAccount))]
  public virtual int? TransitAcctId { get; set; }

  /// <summary>
  /// The special multi-currency asset subaccount used (when necessary) as an intermediate account for currency conversions performed during funds transfers.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
  /// </value>
  [PXDefault]
  [SubAccount(typeof (CASetup.transitAcctId), DisplayName = "Cash-In-Transit Subaccount", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  [PXForeignReference(typeof (CASetup.FK.CashInTransitSubaccount))]
  public virtual int? TransitSubID { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the Control Total box is added to the Summary area of the Transactions (CA304000) form,
  /// so a user can enter the transaction amount manually. This amount will be validated when users enter transactions.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Validate Control Totals on Entry")]
  public virtual bool? RequireControlTotal { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the Tax Amount box is added to the Summary area of the Transactions (CA304000) form,
  /// so a user can enter the tax amount manually in the transaction.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Validate Tax Totals on Entry")]
  public virtual bool? RequireControlTaxTotal { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that newly entered transactions will by default get the On Hold status on entry.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Hold Transactions on Entry")]
  public virtual bool? HoldEntry { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that Accounts Payable documents can be released from the Cash Management module.
  /// If the value of the field is <c>false</c>, Accounts Payable documents can be released only from the Accounts Payable module.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Release AP Documents from CA Module")]
  public virtual bool? ReleaseAP { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that Accounts Receivable documents can be released from the Cash Management module.
  /// If the value of the field is <c>false</c>, Accounts Receivable documents can be released only from the Accounts Receivable module.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Release AR Documents from CA Module")]
  public virtual bool? ReleaseAR { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that unreleased and uncleared receipts are included in calculation of available balances of cash accounts.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Unreleased Uncleared")]
  public virtual bool? CalcBalDebitUnclearedUnreleased { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that unreleased and cleared receipts are included in calculation of available balances of cash accounts.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Unreleased Cleared")]
  public virtual bool? CalcBalDebitClearedUnreleased { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that released and uncleared receipts are included in calculation of available balances of cash accounts.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Released Uncleared")]
  public virtual bool? CalcBalDebitUnclearedReleased { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that unreleased and uncleared disbursements are included in calculation of available balances of cash accounts.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Unreleased Uncleared")]
  public virtual bool? CalcBalCreditUnclearedUnreleased { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that unreleased and cleared disbursements are included in calculation of available balances of cash accounts.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Unreleased Cleared")]
  public virtual bool? CalcBalCreditClearedUnreleased { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that released and uncleared disbursements are included in calculation of available balances of cash accounts.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Released Uncleared")]
  public virtual bool? CalcBalCreditUnclearedReleased { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that cash transactions are automatically posted to General Ledger on release.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? AutoPostOption { get; set; }

  /// <summary>
  /// The range (starting with the current business date) to be used by default on Cash Management reports: Day, Week, Month, or Financial Period.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("W")]
  [PXUIField(DisplayName = "Default Date Range")]
  [CADateRange.List]
  public virtual string DateRangeDefault { get; set; }

  /// <summary>
  /// For the system to categorize a cash transaction as matching, the maximum number of days
  /// between the date of a cash transaction (disbursement) and the selected transaction on the bank statement.
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(5)]
  [PXUIField(DisplayName = "Days Before Bank Transaction Date")]
  public virtual int? ReceiptTranDaysBefore { get; set; }

  /// <summary>
  /// For the system to categorize a cash transaction as matching, the maximum number of days
  /// between the date of the selected transaction on the bank statement and the date of a receipt.
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(5)]
  [PXUIField(DisplayName = "Days After Bank Transaction Date")]
  public virtual int? ReceiptTranDaysAfter { get; set; }

  /// <summary>
  /// For the system to categorize a cash transaction as matching, the maximum number of days
  /// between the date of a cash transaction (receipt) and the selected transaction on the bank statement.
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(5)]
  [PXUIField(DisplayName = "Days Before Bank Transaction Date")]
  public virtual int? DisbursementTranDaysBefore { get; set; }

  /// <summary>
  /// For the system to categorize a cash transaction as matching, the maximum number of days
  /// between the date of the selected transaction on the bank statement and the date of a receipt.
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(5)]
  [PXUIField(DisplayName = "Days After Bank Transaction Date")]
  public virtual int? DisbursementTranDaysAfter { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that credit memo are available for matching to bank transactions on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Matching to Credit Memo")]
  public virtual bool? AllowMatchingCreditMemo { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that debit adjustments are available for matching to bank transactions on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Matching to Debit Adjustment")]
  public virtual bool? AllowMatchingDebitAdjustment { get; set; }

  /// <summary>
  /// The relative weight of the evaluated difference between the reference numbers of the bank transaction and the cash transaction.
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "70.0")]
  [PXUIField(DisplayName = "Ref. Nbr. Weight")]
  public virtual Decimal? RefNbrCompareWeight { get; set; }

  /// <summary>
  /// The relative weight of the evaluated difference between the dates of the bank transaction and the cash transaction.
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "20.0")]
  [PXUIField(DisplayName = "Doc. Date Weight")]
  public virtual Decimal? DateCompareWeight { get; set; }

  /// <summary>
  /// The relative weight of the evaluated difference between the names of the customer (or vendor) on the bank transaction and the cash transaction.
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "10.0")]
  [PXUIField(DisplayName = "Doc. Payee Weight")]
  public virtual Decimal? PayeeCompareWeight { get; set; }

  /// <summary>
  /// The average number of days for a payment to be cleared with the bank.
  /// </summary>
  [PXDBDecimal(MinValue = -365.0, MaxValue = 365.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Payment Clearing Average Delay")]
  public virtual Decimal? DateMeanOffset { get; set; }

  /// <summary>
  /// The number of days before and after the average delay date during which 50% of payments are generally cleared.
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 365.0)]
  [PXDefault(TypeCode.Decimal, "5.0")]
  [PXUIField(DisplayName = "Estimated Deviation (Days)")]
  public virtual Decimal? DateSigma { get; set; }

  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "5.0")]
  [PXUIField(DisplayName = "Amount Difference Threshold (%)")]
  public virtual Decimal? CuryDiffThreshold { get; set; }

  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "10.0")]
  [PXUIField(DisplayName = "Amount Weight")]
  public virtual Decimal? AmountWeight { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that batch payments with empty ref number are available for matching to bank transactions on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? EmptyRefNbrMatching { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the system ignores the currency check when you import bank statements.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Ignore Currency Check on Bank Statement Import")]
  public virtual bool? IgnoreCuryCheckOnImport { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that importing a bank statement from a file should be performed to only a specific cash account.
  /// The user can import the data only after the user has selected the cash account on the Import Bank Transactions (CA306500) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Import Bank Statement to Single Cash Account")]
  public virtual bool? ImportToSingleAccount { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the system ignores the empty FITID value  (the value, which is uploaded to the Ext. Tran. ID column
  /// on the Import Bank Transactions (CA306500) form) when you import the bank statement file.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Empty FITID")]
  public virtual bool? AllowEmptyFITID { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that unreleased batch payments are available for matching to bank transactions on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Matching to Unreleased Batch Payments")]
  public virtual bool? AllowMatchingToUnreleasedBatch { get; set; }

  /// <summary>
  /// The entry type (configured for processing unrecognized payments and for payment reclassification) to be used by default on the Reclassify Payments (CA506500) form.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<CAEntryType.entryTypeId, Where<CAEntryType.module, Equal<BatchModule.moduleCA>, And<CAEntryType.useToReclassifyPayments, Equal<True>>>>), DescriptionField = typeof (CAEntryType.descr))]
  [PXUIField]
  public virtual string UnknownPaymentEntryTypeID { get; set; }

  /// <summary>
  /// The service to be used for importing bank statements if the Import Bank Statements to Single Cash Account check box is cleared.
  /// </summary>
  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Statement Import Service")]
  [PXProviderTypeSelector(new Type[] {typeof (IStatementReader)})]
  public virtual string StatementImportTypeName { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the system skip voided transactions during the reconciliation process if both the original
  /// and the voided transactions are registered in the same financial period.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Skip Voided Transactions")]
  public virtual bool? SkipVoided { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the system skip voided transactions during the reconciliation process if both the original
  /// and the voided transactions are registered in the same financial period.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Skip Reconciled Transactions in Matching")]
  public virtual bool? SkipReconciled { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that users must fill in the Document Ref. box for new cash transactions and deposits.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Require Document Ref. Nbr. on Entry")]
  public virtual bool? RequireExtRefNbr { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the system validates data consistency on the release of cash transactions and deposits.
  /// The validation is described in the <see cref="M:PX.Objects.CA.CAReleaseProcess.CheckMultipleGLPosting(System.Collections.Generic.HashSet{System.Int64})" /> method.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Validate data consistency on Release", Visible = false)]
  public virtual bool? ValidateDataConsistencyOnRelease { get; set; }

  /// <summary>
  /// Absolute Relevance Threshold used in auto-matching of transactions.
  /// Document will be matched automatically to a bank transaction if:
  /// 1. If it's the only match and Match Relevance &gt; Relative Relevance Threshold
  /// 2. There is any number of matches and the best match has Match Relevance &gt; Absolute Relevance Threshold
  /// 3. There is any number of matches and Match Relevance of the best match -  Match Relevance of the second best match&gt;= Relative Relevance Threshold
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "75.0")]
  [PXUIField(DisplayName = "Absolute Relevance Threshold")]
  public virtual Decimal? MatchThreshold { get; set; }

  /// <summary>
  /// Relative Relevance Threshold used in auto-matching of transactions.
  /// Document will be matched automatically to a bank transaction if:
  /// 1. If it's the only match and Match Relevance &gt; Relative Relevance Threshold
  /// 2. There is any number of matches and the best match has Match Relevance &gt; Absolute Relevance Threshold
  /// 3. There is any number of matches and Match Relevance of the best match -  Match Relevance of the second best match&gt;= Relative Relevance Threshold
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "20.0")]
  [PXUIField(DisplayName = "Relative Relevance Threshold")]
  public virtual Decimal? RelativeMatchThreshold { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that invoices will be filtered by dates for matching to bank transactions on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Match by Discount and Due Date")]
  public virtual bool? InvoiceFilterByDate { get; set; }

  /// <summary>
  /// The maximum number of days between the invoice discount date and the date of the selected bank transaction,
  /// to classify invoice as a match
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXUIField(DisplayName = "Days Before Discount Date")]
  public virtual int? DaysBeforeInvoiceDiscountDate { get; set; }

  /// <summary>
  /// The maximum number of days between the date of the selected bank transaction and the invoice due date,
  /// to classify invoice as a match, if bank transaction date earlier than invoice due date
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXUIField(DisplayName = "Days Before Due Date")]
  public virtual int? DaysBeforeInvoiceDueDate { get; set; }

  /// <summary>
  /// The maximum number of days between the invoice due date and the date of the selected bank transaction,
  /// to classify invoice as a match, if bank transaction date later than invoice due date
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXUIField(DisplayName = "Days After Due Date")]
  public virtual int? DaysAfterInvoiceDueDate { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that only Invoices with the same cash account or empty cash account should be selected for matching with bank transactions on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Match by Cash Account")]
  public virtual bool? InvoiceFilterByCashAccount { get; set; }

  /// <summary>
  /// The relative weight of the evaluated difference between the reference numbers of the bank transaction and the invoice.
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "87.5")]
  [PXUIField(DisplayName = "Ref. Nbr. Weight")]
  public virtual Decimal? InvoiceRefNbrCompareWeight { get; set; }

  /// <summary>
  /// The relative weight of the evaluated difference between the dates of the bank transaction and the invoice.
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Doc. Date Weight")]
  public virtual Decimal? InvoiceDateCompareWeight { get; set; }

  /// <summary>
  /// The relative weight of the evaluated difference between the names of the customer on the bank transaction and the invoice.
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "12.5")]
  [PXUIField(DisplayName = "Doc. Payee Weight")]
  public virtual Decimal? InvoicePayeeCompareWeight { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [PXDBDecimal(MinValue = -365.0, MaxValue = 365.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Average Payment Delay")]
  public virtual Decimal? AveragePaymentDelay { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 365.0)]
  [PXDefault(TypeCode.Decimal, "2.0")]
  [PXUIField(DisplayName = "Estimated Deviation (Days)")]
  public virtual Decimal? InvoiceDateSigma { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

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

  public static class FK
  {
    public class BatchNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<CASetup>.By<CASetup.batchNumberingID>
    {
    }

    public class TransactionNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<CASetup>.By<CASetup.registerNumberingID>
    {
    }

    public class TransferNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<CASetup>.By<CASetup.transferNumberingID>
    {
    }

    public class PaymentBatchNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<CASetup>.By<CASetup.cABatchNumberingID>
    {
    }

    public class BankStatementNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<CASetup>.By<CASetup.cAStatementNumberingID>
    {
    }

    public class CorporateCardNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<CASetup>.By<CASetup.corpCardNumberingID>
    {
    }

    public class CashInTransitAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CASetup>.By<CASetup.transitAcctId>
    {
    }

    public class CashInTransitSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CASetup>.By<CASetup.transitSubID>
    {
    }

    public class UnrecognizedReceiptsEntryType : 
      PrimaryKeyOf<CAEntryType>.By<CAEntryType.entryTypeId>.ForeignKeyOf<CASetup>.By<CASetup.unknownPaymentEntryTypeID>
    {
    }
  }

  public abstract class batchNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASetup.batchNumberingID>
  {
  }

  public abstract class registerNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASetup.registerNumberingID>
  {
  }

  public abstract class transferNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASetup.transferNumberingID>
  {
  }

  public abstract class cABatchNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASetup.cABatchNumberingID>
  {
  }

  public abstract class cAStatementNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASetup.cAStatementNumberingID>
  {
  }

  public abstract class corpCardNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASetup.corpCardNumberingID>
  {
  }

  public abstract class transitAcctId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASetup.transitAcctId>
  {
  }

  public abstract class transitSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASetup.transitSubID>
  {
  }

  public abstract class requireControlTotal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.requireControlTotal>
  {
  }

  public abstract class requireControlTaxTotal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.requireControlTaxTotal>
  {
  }

  public abstract class holdEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CASetup.holdEntry>
  {
  }

  public abstract class releaseAP : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CASetup.releaseAP>
  {
  }

  public abstract class releaseAR : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CASetup.releaseAR>
  {
  }

  public abstract class calcBalDebitUnclearedUnreleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.calcBalDebitUnclearedUnreleased>
  {
  }

  public abstract class calcBalDebitClearedUnreleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.calcBalDebitClearedUnreleased>
  {
  }

  public abstract class calcBalDebitUnclearedReleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.calcBalDebitUnclearedReleased>
  {
  }

  public abstract class calcBalCreditUnclearedUnreleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.calcBalCreditUnclearedUnreleased>
  {
  }

  public abstract class calcBalCreditClearedUnreleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.calcBalCreditClearedUnreleased>
  {
  }

  public abstract class calcBalCreditUnclearedReleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.calcBalCreditUnclearedReleased>
  {
  }

  public abstract class autoPostOption : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CASetup.autoPostOption>
  {
  }

  public abstract class dateRangeDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASetup.dateRangeDefault>
  {
  }

  public abstract class receiptTranDaysBefore : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CASetup.receiptTranDaysBefore>
  {
  }

  public abstract class receiptTranDaysAfter : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CASetup.receiptTranDaysAfter>
  {
  }

  public abstract class disbursementTranDaysBefore : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CASetup.disbursementTranDaysBefore>
  {
  }

  public abstract class disbursementTranDaysAfter : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CASetup.disbursementTranDaysAfter>
  {
  }

  public abstract class allowMatchingCreditMemo : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.allowMatchingCreditMemo>
  {
  }

  public abstract class allowMatchingDebitAdjustment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.allowMatchingDebitAdjustment>
  {
  }

  public abstract class refNbrCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CASetup.refNbrCompareWeight>
  {
  }

  public abstract class dateCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CASetup.dateCompareWeight>
  {
  }

  public abstract class payeeCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CASetup.payeeCompareWeight>
  {
  }

  public abstract class dateMeanOffset : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CASetup.dateMeanOffset>
  {
  }

  public abstract class dateSigma : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CASetup.dateSigma>
  {
  }

  public abstract class curyDiffThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CASetup.curyDiffThreshold>
  {
  }

  public abstract class amountWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CASetup.amountWeight>
  {
  }

  public abstract class emptyRefNbrMatching : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.emptyRefNbrMatching>
  {
  }

  public abstract class ignoreCuryCheckOnImport : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.ignoreCuryCheckOnImport>
  {
  }

  public abstract class importToSingleAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.importToSingleAccount>
  {
  }

  public abstract class allowEmptyFITID : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CASetup.allowEmptyFITID>
  {
  }

  public abstract class allowMatchingToUnreleasedBatch : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.allowMatchingToUnreleasedBatch>
  {
  }

  public abstract class unknownPaymentEntryTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASetup.unknownPaymentEntryTypeID>
  {
  }

  public abstract class statementImportTypeName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASetup.statementImportTypeName>
  {
  }

  public abstract class skipVoided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CASetup.skipVoided>
  {
  }

  public abstract class skipReconciled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CASetup.skipReconciled>
  {
  }

  public abstract class requireExtRefNbr : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CASetup.requireExtRefNbr>
  {
  }

  public abstract class validateDataConsistencyOnRelease : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.validateDataConsistencyOnRelease>
  {
  }

  public abstract class matchThreshold : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CASetup.matchThreshold>
  {
  }

  public abstract class relativeMatchThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CASetup.relativeMatchThreshold>
  {
  }

  public abstract class invoiceFilterByDate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.invoiceFilterByDate>
  {
  }

  public abstract class daysBeforeInvoiceDiscountDate : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CASetup.daysBeforeInvoiceDiscountDate>
  {
  }

  public abstract class daysBeforeInvoiceDueDate : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CASetup.daysBeforeInvoiceDueDate>
  {
  }

  public abstract class daysAfterInvoiceDueDate : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CASetup.daysAfterInvoiceDueDate>
  {
  }

  public abstract class invoiceFilterByCashAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CASetup.invoiceFilterByCashAccount>
  {
  }

  public abstract class invoiceRefNbrCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CASetup.invoiceRefNbrCompareWeight>
  {
  }

  public abstract class invoiceDateCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CASetup.invoiceDateCompareWeight>
  {
  }

  public abstract class invoicePayeeCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CASetup.invoicePayeeCompareWeight>
  {
  }

  public abstract class averagePaymentDelay : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CASetup.averagePaymentDelay>
  {
  }

  public abstract class invoiceDateSigma : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CASetup.invoiceDateSigma>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CASetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CASetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CASetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CASetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CASetup.lastModifiedDateTime>
  {
  }
}
