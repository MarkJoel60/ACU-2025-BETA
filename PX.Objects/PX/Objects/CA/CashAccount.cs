// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashAccount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CA.BankStatementHelpers;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>The details and settings of cash accounts.</summary>
[PXCacheName("Cash Account")]
[PXPrimaryGraph(new Type[] {typeof (CashAccountMaint)}, new Type[] {typeof (Select<CashAccount, Where<CashAccount.cashAccountID, Equal<Current<CashAccount.cashAccountID>>>>)})]
[PXGroupMask(typeof (InnerJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<CashAccount.accountID>, And<Match<PX.Objects.GL.Account, Current<AccessInfo.userName>>>>, InnerJoin<PX.Objects.GL.Sub, On<PX.Objects.GL.Sub.subID, Equal<CashAccount.subID>, And<Match<PX.Objects.GL.Sub, Current<AccessInfo.userName>>>>>>))]
[Serializable]
public class CashAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IMatchSettings
{
  protected bool? _Selected = new bool?(false);
  protected bool? _SkipVoided;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? Active { get; set; }

  [PXDBIdentity]
  [PXUIField(Enabled = false)]
  [PXReferentialIntegrityCheck]
  public virtual int? CashAccountID { get; set; }

  [CashAccountRaw]
  [PXDefault]
  public virtual 
  #nullable disable
  string CashAccountCD { get; set; }

  [PXDefault]
  [Account]
  [PXRestrictor(typeof (Where<PX.Objects.GL.Account.controlAccountModule, IsNull>), "Only a non-control account can be selected as a cash account.", new Type[] {})]
  public virtual int? AccountID { get; set; }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDefault]
  [SubAccount(typeof (CashAccount.accountID))]
  public virtual int? SubID { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Descr { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  [PXDefault]
  public virtual string CuryID { get; set; }

  [PXDBString(6, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.CurrencyRateType.curyRateTypeID))]
  [PXForeignReference(typeof (Field<CashAccount.curyRateTypeID>.IsRelatedTo<PX.Objects.CM.CurrencyRateType.curyRateTypeID>))]
  [PXUIField(DisplayName = "Curr. Rate Type")]
  public virtual string CuryRateTypeID { get; set; }

  /// <summary>
  /// If set to <c>true</c>, indicates that the currency
  /// of customer documents (which is specified by <see cref="P:PX.Objects.CA.CashAccount.CuryID" />)
  /// can be overridden by a user during document entry.
  /// /// </summary>
  [PXBool]
  [PXFormula(typeof (CashAccount.allowOverrideCury.Disabled))]
  [PXUIField(DisplayName = "Enable Currency Override")]
  public virtual bool? AllowOverrideCury { get; set; }

  /// <summary>
  /// If set to <c>true</c>, indicates that the currency rate
  /// for customer documents (which is calculated by the system
  /// from the currency rate history) can be overridden by a user
  /// during document entry.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (CashAccount.allowOverrideRate.Enabled))]
  [PXUIField(DisplayName = "Enable Rate Override")]
  public virtual bool? AllowOverrideRate { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField]
  public virtual string ExtRefNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Requires Reconciliation")]
  public virtual bool? Reconcile { get; set; }

  [PXDefault]
  [Vendor(DescriptionField = typeof (PX.Objects.AP.Vendor.acctName), DisplayName = "Bank ID")]
  [PXUIField(DisplayName = "Bank ID")]
  public virtual int? ReferenceID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Reconciliation Numbering Sequence", Required = false)]
  public virtual string ReconNumberingID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Clearing Account")]
  public virtual bool? ClearingAccount { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Signature")]
  public virtual string Signature { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Name")]
  public virtual string SignatureDescr { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Statement Import Service")]
  [PXProviderTypeSelector(new Type[] {typeof (IStatementReader)})]
  public virtual string StatementImportTypeName { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Restrict Visibility with Branch")]
  public virtual bool? RestrictVisibilityWithBranch { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Cards Allowed", Visible = false, Enabled = false)]
  public virtual bool? PTInstancesAllowed { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Account Settings Allowed", Visible = false, Enabled = false)]
  public virtual bool? AcctSettingsAllowed { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Match Bank Transactions to Batch Payments")]
  public virtual bool? MatchToBatch { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use for Corporate Cards")]
  public virtual bool? UseForCorpCard { get; set; }

  [PXDBString(5, IsUnicode = true)]
  [PXDefault]
  public virtual string BaseCuryID { get; set; }

  /// <summary>Gets sets ReceiptTranDaysBefore</summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(5, typeof (CASetup.receiptTranDaysBefore))]
  [PXUIField(DisplayName = "Days Before Bank Transaction Date")]
  public virtual int? ReceiptTranDaysBefore { get; set; }

  /// <summary>Gets sets ReceiptTranDaysAfter</summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(2, typeof (CASetup.receiptTranDaysAfter))]
  [PXUIField(DisplayName = "Days After Bank Transaction Date")]
  public virtual int? ReceiptTranDaysAfter { get; set; }

  /// <summary>Gets sets DisbursementTranDaysBefore</summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(5, typeof (CASetup.disbursementTranDaysBefore))]
  [PXUIField(DisplayName = "Days Before Bank Transaction Date")]
  public virtual int? DisbursementTranDaysBefore { get; set; }

  /// <summary>Gets sets DisbursementTranDaysAfter</summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(2, typeof (CASetup.disbursementTranDaysAfter))]
  [PXUIField(DisplayName = "Days After Bank Transaction Date")]
  public virtual int? DisbursementTranDaysAfter { get; set; }

  /// <summary>Gets sets AllowMatchingCreditMemo</summary>
  [PXDBBool]
  [PXDefault(false, typeof (CASetup.allowMatchingCreditMemo))]
  [PXUIField(DisplayName = "Allow Matching to Credit Memo")]
  public virtual bool? AllowMatchingCreditMemo { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that debit adjustments are available for matching to bank transactions on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false, typeof (CASetup.allowMatchingDebitAdjustment))]
  [PXUIField(DisplayName = "Allow Matching to Debit Adjustment")]
  public virtual bool? AllowMatchingDebitAdjustment { get; set; }

  /// <summary>Gets sets RefNbrCompareWeight</summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "70.0", typeof (CASetup.refNbrCompareWeight))]
  [PXUIField(DisplayName = "Ref. Nbr. Weight")]
  public virtual Decimal? RefNbrCompareWeight { get; set; }

  /// <summary>Gets sets DateCompareWeight</summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "20.0", typeof (CASetup.dateCompareWeight))]
  [PXUIField(DisplayName = "Doc. Date Weight")]
  public virtual Decimal? DateCompareWeight { get; set; }

  /// <summary>Gets sets PayeeCompareWeight</summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "10.0", typeof (CASetup.payeeCompareWeight))]
  [PXUIField(DisplayName = "Doc. Payee Weight")]
  public virtual Decimal? PayeeCompareWeight { get; set; }

  protected Decimal TotalWeight
  {
    get
    {
      Decimal? nullable = this.DateCompareWeight;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.RefNbrCompareWeight;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      nullable = this.PayeeCompareWeight;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return num + valueOrDefault3;
    }
  }

  /// <summary>Gets sets RefNbrComparePercent</summary>
  [PXDecimal]
  [PXUIField(DisplayName = "%", Enabled = false)]
  public virtual Decimal? RefNbrComparePercent
  {
    get
    {
      Decimal totalWeight = this.TotalWeight;
      Decimal? nullable1;
      if (!(totalWeight != 0M))
      {
        nullable1 = new Decimal?(0M);
      }
      else
      {
        Decimal? nbrCompareWeight = this.RefNbrCompareWeight;
        Decimal num = totalWeight;
        nullable1 = nbrCompareWeight.HasValue ? new Decimal?(nbrCompareWeight.GetValueOrDefault() / num) : new Decimal?();
      }
      Decimal? nullable2 = nullable1;
      Decimal num1 = 100.0M;
      return !nullable2.HasValue ? new Decimal?() : new Decimal?(nullable2.GetValueOrDefault() * num1);
    }
    set
    {
    }
  }

  /// <summary>Gets sets EmptyRefNbrMatching</summary>
  [PXDBBool]
  [PXDefault(false, typeof (CASetup.emptyRefNbrMatching))]
  [PXUIField]
  public virtual bool? EmptyRefNbrMatching { get; set; }

  /// <summary>Gets sets DateComparePercent</summary>
  [PXDecimal]
  [PXUIField(DisplayName = "%", Enabled = false)]
  public virtual Decimal? DateComparePercent
  {
    get
    {
      Decimal totalWeight = this.TotalWeight;
      Decimal? nullable1;
      if (!(totalWeight != 0M))
      {
        nullable1 = new Decimal?(0M);
      }
      else
      {
        Decimal? dateCompareWeight = this.DateCompareWeight;
        Decimal num = totalWeight;
        nullable1 = dateCompareWeight.HasValue ? new Decimal?(dateCompareWeight.GetValueOrDefault() / num) : new Decimal?();
      }
      Decimal? nullable2 = nullable1;
      Decimal num1 = 100.0M;
      return !nullable2.HasValue ? new Decimal?() : new Decimal?(nullable2.GetValueOrDefault() * num1);
    }
    set
    {
    }
  }

  /// <summary>Gets sets PayeeComparePercent</summary>
  [PXDecimal]
  [PXUIField(DisplayName = "%", Enabled = false)]
  public virtual Decimal? PayeeComparePercent
  {
    get
    {
      Decimal totalWeight = this.TotalWeight;
      Decimal? nullable1;
      if (!(totalWeight != 0M))
      {
        nullable1 = new Decimal?(0M);
      }
      else
      {
        Decimal? payeeCompareWeight = this.PayeeCompareWeight;
        Decimal num = totalWeight;
        nullable1 = payeeCompareWeight.HasValue ? new Decimal?(payeeCompareWeight.GetValueOrDefault() / num) : new Decimal?();
      }
      Decimal? nullable2 = nullable1;
      Decimal num1 = 100.0M;
      return !nullable2.HasValue ? new Decimal?() : new Decimal?(nullable2.GetValueOrDefault() * num1);
    }
    set
    {
    }
  }

  /// <summary>Gets sets DateMeanOffset</summary>
  [PXDBDecimal(MinValue = -365.0, MaxValue = 365.0)]
  [PXDefault(TypeCode.Decimal, "10.0", typeof (CASetup.dateMeanOffset))]
  [PXUIField(DisplayName = "Payment Clearing Average Delay")]
  public virtual Decimal? DateMeanOffset { get; set; }

  /// <summary>Gets sets DateSigma</summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 365.0)]
  [PXDefault(TypeCode.Decimal, "5.0", typeof (CASetup.dateSigma))]
  [PXUIField(DisplayName = "Estimated Deviation (Days)")]
  public virtual Decimal? DateSigma { get; set; }

  /// <summary>Gets sets SkipVoided</summary>
  [PXDBBool]
  [PXDefault(false, typeof (CASetup.skipVoided))]
  [PXUIField(DisplayName = "Skip Voided Transactions During Matching")]
  public virtual bool? SkipVoided
  {
    get => this._SkipVoided;
    set => this._SkipVoided = value;
  }

  /// <summary>Gets sets CuryDiffThreshold</summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "5.0", typeof (CASetup.curyDiffThreshold))]
  [PXUIField(DisplayName = "Amount Difference Threshold (%)")]
  public virtual Decimal? CuryDiffThreshold { get; set; }

  /// <summary>Gets sets AmountWeight</summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "10.0", typeof (CASetup.amountWeight))]
  [PXUIField(DisplayName = "Amount Weight")]
  public virtual Decimal? AmountWeight { get; set; }

  protected Decimal ExpenseReceiptTotalWeight
  {
    get
    {
      Decimal? nullable = this.DateCompareWeight;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.RefNbrCompareWeight;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      nullable = this.AmountWeight;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return num + valueOrDefault3;
    }
  }

  /// <summary>Gets sets ExpenseReceiptRefNbrComparePercent</summary>
  [PXDecimal]
  public virtual Decimal? ExpenseReceiptRefNbrComparePercent
  {
    get
    {
      Decimal receiptTotalWeight = this.ExpenseReceiptTotalWeight;
      Decimal? nullable1;
      if (!(receiptTotalWeight != 0M))
      {
        nullable1 = new Decimal?(0M);
      }
      else
      {
        Decimal? nbrCompareWeight = this.RefNbrCompareWeight;
        Decimal num = receiptTotalWeight;
        nullable1 = nbrCompareWeight.HasValue ? new Decimal?(nbrCompareWeight.GetValueOrDefault() / num) : new Decimal?();
      }
      Decimal? nullable2 = nullable1;
      Decimal num1 = 100.0M;
      return !nullable2.HasValue ? new Decimal?() : new Decimal?(nullable2.GetValueOrDefault() * num1);
    }
    set
    {
    }
  }

  /// <summary>Gets sets ExpenseReceiptDateComparePercent</summary>
  [PXDecimal]
  public virtual Decimal? ExpenseReceiptDateComparePercent
  {
    get
    {
      Decimal receiptTotalWeight = this.ExpenseReceiptTotalWeight;
      Decimal? nullable1;
      if (!(receiptTotalWeight != 0M))
      {
        nullable1 = new Decimal?(0M);
      }
      else
      {
        Decimal? dateCompareWeight = this.DateCompareWeight;
        Decimal num = receiptTotalWeight;
        nullable1 = dateCompareWeight.HasValue ? new Decimal?(dateCompareWeight.GetValueOrDefault() / num) : new Decimal?();
      }
      Decimal? nullable2 = nullable1;
      Decimal num1 = 100.0M;
      return !nullable2.HasValue ? new Decimal?() : new Decimal?(nullable2.GetValueOrDefault() * num1);
    }
    set
    {
    }
  }

  /// <summary>Gets sets ExpenseReceiptAmountComparePercent</summary>
  [PXDecimal]
  public virtual Decimal? ExpenseReceiptAmountComparePercent
  {
    get
    {
      Decimal receiptTotalWeight = this.ExpenseReceiptTotalWeight;
      Decimal? nullable1;
      if (!(receiptTotalWeight != 0M))
      {
        nullable1 = new Decimal?(0M);
      }
      else
      {
        Decimal? amountWeight = this.AmountWeight;
        Decimal num = receiptTotalWeight;
        nullable1 = amountWeight.HasValue ? new Decimal?(amountWeight.GetValueOrDefault() / num) : new Decimal?();
      }
      Decimal? nullable2 = nullable1;
      Decimal num1 = 100.0M;
      return !nullable2.HasValue ? new Decimal?() : new Decimal?(nullable2.GetValueOrDefault() * num1);
    }
    set
    {
    }
  }

  /// <summary>Gets sets RatioInRelevanceCalculationLabel</summary>
  [PXString]
  [PXUIField]
  public virtual string RatioInRelevanceCalculationLabel
  {
    get
    {
      return PXMessages.LocalizeFormatNoPrefix("For expense receipts, the weights of the Ref. Nbr., Doc. Date, and Amount in the relevance calculation are {0:F2}%, {1:F2}%, and {2:F2}%, respectively.", new object[3]
      {
        (object) this.ExpenseReceiptRefNbrComparePercent,
        (object) this.ExpenseReceiptDateComparePercent,
        (object) this.ExpenseReceiptAmountComparePercent
      });
    }
    set
    {
    }
  }

  /// <summary>Gets sets MatchSettingsPerAccount</summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? MatchSettingsPerAccount { get; set; }

  /// <summary>
  /// Absolute Relevance Threshold used in auto-matching of transactions.
  /// Document will be matched automatically to a bank transaction if:
  /// 1. If it's the only match and Match Relevance &gt; Relative Relevance Threshold
  /// 2. There is any number of matches and the best match has Match Relevance &gt; Absolute Relevance Threshold
  /// 3. There is any number of matches and Match Relevance of the best match -  Match Relevance of the second best match&gt;= Relative Relevance Threshold
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "75.0", typeof (CASetup.matchThreshold))]
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
  [PXDefault(TypeCode.Decimal, "20.0", typeof (CASetup.relativeMatchThreshold))]
  [PXUIField(DisplayName = "Relative Relevance Threshold")]
  public virtual Decimal? RelativeMatchThreshold { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that invoices will be filtered by dates for matching to bank transactions on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false, typeof (CASetup.invoiceFilterByDate))]
  [PXUIField(DisplayName = "Match by Discount and Due Date")]
  public virtual bool? InvoiceFilterByDate { get; set; }

  /// <summary>
  /// The maximum number of days between the invoice discount date and the date of the selected bank transaction,
  /// to classify invoice as a match
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(typeof (CASetup.daysBeforeInvoiceDiscountDate))]
  [PXUIField(DisplayName = "Days Before Discount Date")]
  public virtual int? DaysBeforeInvoiceDiscountDate { get; set; }

  /// <summary>
  /// The maximum number of days between the date of the selected bank transaction and the invoice due date,
  /// to classify invoice as a match, if bank transaction date earlier than invoice due date
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(typeof (CASetup.daysBeforeInvoiceDueDate))]
  [PXUIField(DisplayName = "Days Before Due Date")]
  public virtual int? DaysBeforeInvoiceDueDate { get; set; }

  /// <summary>
  /// The maximum number of days between the invoice due date and the date of the selected bank transaction,
  /// to classify invoice as a match, if bank transaction date later than invoice due date
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(typeof (CASetup.daysAfterInvoiceDueDate))]
  [PXUIField(DisplayName = "Days After Due Date")]
  public virtual int? DaysAfterInvoiceDueDate { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that only Invoices with the same cash account or empty cash account should be selected for matching with bank transactions on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false, typeof (CASetup.invoiceFilterByCashAccount))]
  [PXUIField(DisplayName = "Match by Cash Account")]
  public virtual bool? InvoiceFilterByCashAccount { get; set; }

  /// <summary>
  /// The relative weight of the evaluated difference between the reference numbers of the bank transaction and the invoice.
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "87.5", typeof (CASetup.invoiceRefNbrCompareWeight))]
  [PXUIField(DisplayName = "Ref. Nbr. Weight")]
  public virtual Decimal? InvoiceRefNbrCompareWeight { get; set; }

  /// <summary>
  /// The relative weight of the evaluated difference between the dates of the bank transaction and the invoice.
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (CASetup.invoiceDateCompareWeight))]
  [PXUIField(DisplayName = "Doc. Date Weight")]
  public virtual Decimal? InvoiceDateCompareWeight { get; set; }

  /// <summary>
  /// The relative weight of the evaluated difference between the names of the customer on the bank transaction and the invoice.
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "12.5", typeof (CASetup.invoicePayeeCompareWeight))]
  [PXUIField(DisplayName = "Doc. Payee Weight")]
  public virtual Decimal? InvoicePayeeCompareWeight { get; set; }

  protected Decimal InvoiceTotalWeight
  {
    get
    {
      Decimal? nullable = this.InvoiceDateCompareWeight;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.InvoiceRefNbrCompareWeight;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num = valueOrDefault1 + valueOrDefault2;
      nullable = this.InvoicePayeeCompareWeight;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      return num + valueOrDefault3;
    }
  }

  /// <summary>Gets sets RefNbrComparePercent</summary>
  [PXDecimal]
  [PXUIField(DisplayName = "%", Enabled = false)]
  public virtual Decimal? InvoiceRefNbrComparePercent
  {
    get
    {
      Decimal invoiceTotalWeight = this.InvoiceTotalWeight;
      Decimal? nullable1;
      if (!(invoiceTotalWeight != 0M))
      {
        nullable1 = new Decimal?(0M);
      }
      else
      {
        Decimal? nbrCompareWeight = this.InvoiceRefNbrCompareWeight;
        Decimal num = invoiceTotalWeight;
        nullable1 = nbrCompareWeight.HasValue ? new Decimal?(nbrCompareWeight.GetValueOrDefault() / num) : new Decimal?();
      }
      Decimal? nullable2 = nullable1;
      Decimal num1 = 100.0M;
      return !nullable2.HasValue ? new Decimal?() : new Decimal?(nullable2.GetValueOrDefault() * num1);
    }
    set
    {
    }
  }

  /// <summary>Gets sets InvoiceDateComparePercent</summary>
  [PXDecimal]
  [PXUIField(DisplayName = "%", Enabled = false)]
  public virtual Decimal? InvoiceDateComparePercent
  {
    get
    {
      Decimal invoiceTotalWeight = this.InvoiceTotalWeight;
      Decimal? nullable1;
      if (!(invoiceTotalWeight != 0M))
      {
        nullable1 = new Decimal?(0M);
      }
      else
      {
        Decimal? dateCompareWeight = this.InvoiceDateCompareWeight;
        Decimal num = invoiceTotalWeight;
        nullable1 = dateCompareWeight.HasValue ? new Decimal?(dateCompareWeight.GetValueOrDefault() / num) : new Decimal?();
      }
      Decimal? nullable2 = nullable1;
      Decimal num1 = 100.0M;
      return !nullable2.HasValue ? new Decimal?() : new Decimal?(nullable2.GetValueOrDefault() * num1);
    }
    set
    {
    }
  }

  /// <summary>Gets sets InvoicePayeeComparePercent</summary>
  [PXDecimal]
  [PXUIField(DisplayName = "%", Enabled = false)]
  public virtual Decimal? InvoicePayeeComparePercent
  {
    get
    {
      Decimal invoiceTotalWeight = this.InvoiceTotalWeight;
      Decimal? nullable1;
      if (!(invoiceTotalWeight != 0M))
      {
        nullable1 = new Decimal?(0M);
      }
      else
      {
        Decimal? payeeCompareWeight = this.InvoicePayeeCompareWeight;
        Decimal num = invoiceTotalWeight;
        nullable1 = payeeCompareWeight.HasValue ? new Decimal?(payeeCompareWeight.GetValueOrDefault() / num) : new Decimal?();
      }
      Decimal? nullable2 = nullable1;
      Decimal num1 = 100.0M;
      return !nullable2.HasValue ? new Decimal?() : new Decimal?(nullable2.GetValueOrDefault() * num1);
    }
    set
    {
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [PXDBDecimal(MinValue = -365.0, MaxValue = 365.0)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (CASetup.averagePaymentDelay))]
  [PXUIField(DisplayName = "Average Payment Delay")]
  public virtual Decimal? AveragePaymentDelay { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 365.0)]
  [PXDefault(TypeCode.Decimal, "2.0", typeof (CASetup.invoiceDateSigma))]
  [PXUIField(DisplayName = "Estimated Deviation (Days)")]
  public virtual Decimal? InvoiceDateSigma { get; set; }

  [PXNote(DescriptionField = typeof (CashAccount.cashAccountCD))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>
  {
    public static CashAccount Find(PXGraph graph, int? cashAccountID, PKFindOptions options = 0)
    {
      return (CashAccount) PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.FindBy(graph, (object) cashAccountID, options);
    }
  }

  public static class FK
  {
    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CashAccount>.By<CashAccount.accountID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CashAccount>.By<CashAccount.branchID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CashAccount>.By<CashAccount.subID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CashAccount>.By<CashAccount.curyID>
    {
    }

    public class Bank : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<CashAccount>.By<CashAccount.referenceID>
    {
    }

    public class ReconciliationNumberingSequence : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<CashAccount>.By<CashAccount.reconNumberingID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CashAccount.selected>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CashAccount.active>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashAccount.cashAccountID>
  {
  }

  public abstract class cashAccountCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashAccount.cashAccountCD>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashAccount.accountID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashAccount.branchID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashAccount.subID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashAccount.descr>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashAccount.curyID>
  {
  }

  public abstract class curyRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccount.curyRateTypeID>
  {
  }

  public abstract class allowOverrideCury : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CashAccount.allowOverrideCury>
  {
    public class Disabled : Case<Where<True, Equal<True>>, False>
    {
    }
  }

  public abstract class allowOverrideRate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CashAccount.allowOverrideRate>
  {
    public class Enabled : Case<Where<True, Equal<True>>, True>
    {
    }
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashAccount.extRefNbr>
  {
  }

  public abstract class reconcile : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CashAccount.reconcile>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashAccount.referenceID>
  {
  }

  public abstract class reconNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccount.reconNumberingID>
  {
  }

  public abstract class clearingAccount : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CashAccount.clearingAccount>
  {
  }

  public abstract class signature : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashAccount.signature>
  {
  }

  public abstract class signatureDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccount.signatureDescr>
  {
  }

  public abstract class statementImportTypeName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccount.statementImportTypeName>
  {
  }

  public abstract class restrictVisibilityWithBranch : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CashAccount.restrictVisibilityWithBranch>
  {
  }

  public abstract class pTInstancesAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CashAccount.pTInstancesAllowed>
  {
  }

  public abstract class acctSettingsAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CashAccount.acctSettingsAllowed>
  {
  }

  public abstract class matchToBatch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CashAccount.matchToBatch>
  {
  }

  public abstract class useForCorpCard : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CashAccount.useForCorpCard>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashAccount.baseCuryID>
  {
  }

  /// <summary>Gets sets ReceiptTranDaysBefore</summary>
  public abstract class receiptTranDaysBefore : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CashAccount.receiptTranDaysBefore>
  {
  }

  /// <summary>Gets sets ReceiptTranDaysAfter</summary>
  public abstract class receiptTranDaysAfter : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CashAccount.receiptTranDaysAfter>
  {
  }

  /// <summary>Gets sets DisbursementTranDaysBefore</summary>
  public abstract class disbursementTranDaysBefore : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CashAccount.disbursementTranDaysBefore>
  {
  }

  /// <summary>Gets sets DisbursementTranDaysAfter</summary>
  public abstract class disbursementTranDaysAfter : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CashAccount.disbursementTranDaysAfter>
  {
  }

  /// <summary>Gets sets AllowMatchingCreditMemo</summary>
  public abstract class allowMatchingCreditMemo : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CashAccount.allowMatchingCreditMemo>
  {
  }

  public abstract class allowMatchingDebitAdjustment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CashAccount.allowMatchingDebitAdjustment>
  {
  }

  /// <summary>Gets sets RefNbrCompareWeight</summary>
  public abstract class refNbrCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.refNbrCompareWeight>
  {
  }

  /// <summary>Gets sets DateCompareWeight</summary>
  public abstract class dateCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.dateCompareWeight>
  {
  }

  /// <summary>Gets sets PayeeCompareWeight</summary>
  public abstract class payeeCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.payeeCompareWeight>
  {
  }

  /// <summary>Gets sets RefNbrComparePercent</summary>
  public abstract class refNbrComparePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.refNbrComparePercent>
  {
  }

  /// <summary>Gets sets EmptyRefNbrMatching</summary>
  public abstract class emptyRefNbrMatching : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CashAccount.emptyRefNbrMatching>
  {
  }

  /// <summary>Gets sets DateComparePercent</summary>
  public abstract class dateComparePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.dateComparePercent>
  {
  }

  /// <summary>Gets sets PayeeComparePercent</summary>
  public abstract class payeeComparePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.payeeComparePercent>
  {
  }

  /// <summary>Gets sets DateMeanOffset</summary>
  public abstract class dateMeanOffset : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.dateMeanOffset>
  {
  }

  /// <summary>Gets sets DateSigma</summary>
  public abstract class dateSigma : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CashAccount.dateSigma>
  {
  }

  /// <summary>Gets sets SkipVoided</summary>
  public abstract class skipVoided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CashAccount.skipVoided>
  {
  }

  /// <summary>Gets sets CuryDiffThreshold</summary>
  public abstract class curyDiffThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.curyDiffThreshold>
  {
  }

  /// <summary>Gets sets AmountWeight</summary>
  public abstract class amountWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CashAccount.amountWeight>
  {
  }

  /// <summary>Gets sets ExpenseReceiptRefNbrComparePercent</summary>
  public abstract class expenseReceiptRefNbrComparePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.expenseReceiptRefNbrComparePercent>
  {
  }

  /// <summary>Gets sets ExpenseReceiptDateComparePercent</summary>
  public abstract class expenseReceiptDateComparePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.expenseReceiptDateComparePercent>
  {
  }

  /// <summary>Gets sets ExpenseReceiptAmountComparePercent</summary>
  public abstract class expenseReceiptAmountComparePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.expenseReceiptAmountComparePercent>
  {
  }

  /// <summary>Gets sets RatioInRelevanceCalculationLabel</summary>
  public abstract class ratioInRelevanceCalculationLabel : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.ratioInRelevanceCalculationLabel>
  {
  }

  /// <summary>Gets sets MatchSettingsPerAccount</summary>
  public abstract class matchSettingsPerAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CashAccount.matchSettingsPerAccount>
  {
  }

  public abstract class matchThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.matchThreshold>
  {
  }

  public abstract class relativeMatchThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.relativeMatchThreshold>
  {
  }

  public abstract class invoiceFilterByDate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CashAccount.invoiceFilterByDate>
  {
  }

  public abstract class daysBeforeInvoiceDiscountDate : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CashAccount.daysBeforeInvoiceDiscountDate>
  {
  }

  public abstract class daysBeforeInvoiceDueDate : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CashAccount.daysBeforeInvoiceDueDate>
  {
  }

  public abstract class daysAfterInvoiceDueDate : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CashAccount.daysAfterInvoiceDueDate>
  {
  }

  public abstract class invoiceFilterByCashAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CashAccount.invoiceFilterByCashAccount>
  {
  }

  public abstract class invoiceRefNbrCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.invoiceRefNbrCompareWeight>
  {
  }

  public abstract class invoiceDateCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.invoiceDateCompareWeight>
  {
  }

  public abstract class invoicePayeeCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.invoicePayeeCompareWeight>
  {
  }

  /// <summary>Gets sets InvoiceRefNbrComparePercent</summary>
  public abstract class invoiceRefNbrComparePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.invoiceRefNbrComparePercent>
  {
  }

  /// <summary>Gets sets InvoiceDateComparePercent</summary>
  public abstract class invoiceDateComparePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.invoiceDateComparePercent>
  {
  }

  /// <summary>Gets sets InvoicePayeeComparePercent</summary>
  public abstract class invoicePayeeComparePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.invoicePayeeComparePercent>
  {
  }

  public abstract class averagePaymentDelay : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.averagePaymentDelay>
  {
  }

  public abstract class invoiceDateSigma : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccount.invoiceDateSigma>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CashAccount.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CashAccount.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CashAccount.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccount.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CashAccount.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CashAccount.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccount.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CashAccount.lastModifiedDateTime>
  {
  }
}
