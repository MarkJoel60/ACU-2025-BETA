// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankStatementHelpers.MatchSettings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CA.BankStatementHelpers;

[Serializable]
public class MatchSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IMatchSettings
{
  protected int? _ReceiptTranDaysBefore;
  protected int? _ReceiptTranDaysAfter;
  protected int? _DisbursementTranDaysBefore;
  protected int? _DisbursementTranDaysAfter;
  protected Decimal? _RefNbrCompareWeight;
  protected Decimal? _DateCompareWeight;
  protected Decimal? _PayeeCompareWeight;
  protected Decimal? _DateMeanOffset;
  protected Decimal? _DateSigma;
  protected bool? _SkipVoided;

  [PXInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(5, typeof (CASetup.receiptTranDaysBefore))]
  [PXUIField(DisplayName = "Days Before Bank Transaction Date")]
  public virtual int? ReceiptTranDaysBefore
  {
    get => this._ReceiptTranDaysBefore;
    set => this._ReceiptTranDaysBefore = value;
  }

  [PXInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(2, typeof (CASetup.receiptTranDaysAfter))]
  [PXUIField(DisplayName = "Days After Bank Transaction Date")]
  public virtual int? ReceiptTranDaysAfter
  {
    get => this._ReceiptTranDaysAfter;
    set => this._ReceiptTranDaysAfter = value;
  }

  [PXInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(5, typeof (CASetup.disbursementTranDaysBefore))]
  [PXUIField(DisplayName = "Days Before Bank Transaction Date")]
  public virtual int? DisbursementTranDaysBefore
  {
    get => this._DisbursementTranDaysBefore;
    set => this._DisbursementTranDaysBefore = value;
  }

  [PXInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(2, typeof (CASetup.disbursementTranDaysAfter))]
  [PXUIField(DisplayName = "Days After Bank Transaction Date")]
  public virtual int? DisbursementTranDaysAfter
  {
    get => this._DisbursementTranDaysAfter;
    set => this._DisbursementTranDaysAfter = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Matching to Credit Memo")]
  public virtual bool? AllowMatchingCreditMemo { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that debit adjustments are available for matching to bank transactions on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false, typeof (CASetup.allowMatchingDebitAdjustment))]
  [PXUIField(DisplayName = "Allow Matching to Debit Adjustment")]
  public virtual bool? AllowMatchingDebitAdjustment { get; set; }

  [PXDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "70.0")]
  [PXUIField(DisplayName = "Ref. Nbr. Weight")]
  public virtual Decimal? RefNbrCompareWeight
  {
    get => this._RefNbrCompareWeight;
    set => this._RefNbrCompareWeight = value;
  }

  [PXDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "20.0")]
  [PXUIField(DisplayName = "Doc. Date Weight")]
  public virtual Decimal? DateCompareWeight
  {
    get => this._DateCompareWeight;
    set => this._DateCompareWeight = value;
  }

  [PXDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "10.0")]
  [PXUIField(DisplayName = "Doc. Payee Weight")]
  public virtual Decimal? PayeeCompareWeight
  {
    get => this._PayeeCompareWeight;
    set => this._PayeeCompareWeight = value;
  }

  protected Decimal TotalWeight
  {
    get
    {
      return this._DateCompareWeight.GetValueOrDefault() + this.RefNbrCompareWeight.GetValueOrDefault() + this.PayeeCompareWeight.GetValueOrDefault();
    }
  }

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

  [PXBool]
  [PXDefault(false, typeof (CASetup.emptyRefNbrMatching))]
  [PXUIField]
  public virtual bool? EmptyRefNbrMatching { get; set; }

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

  [PXDecimal(MinValue = -365.0, MaxValue = 365.0)]
  [PXDefault(TypeCode.Decimal, "10.0")]
  [PXUIField(DisplayName = "Payment Clearing Average Delay")]
  public virtual Decimal? DateMeanOffset
  {
    get => this._DateMeanOffset;
    set => this._DateMeanOffset = value;
  }

  [PXDecimal(MinValue = 0.0, MaxValue = 365.0)]
  [PXDefault(TypeCode.Decimal, "5.0")]
  [PXUIField(DisplayName = "Estimated Deviation (Days)")]
  public virtual Decimal? DateSigma
  {
    get => this._DateSigma;
    set => this._DateSigma = value;
  }

  [PXBool]
  [PXDefault(false, typeof (CASetup.skipVoided))]
  [PXUIField(DisplayName = "Skip Voided Transactions During Matching")]
  public virtual bool? SkipVoided
  {
    get => this._SkipVoided;
    set => this._SkipVoided = value;
  }

  public Decimal? AmountWeight { get; set; }

  public Decimal? CuryDiffThreshold { get; set; }

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
  [PXUIField(DisplayName = "Filter by Date")]
  public virtual bool? InvoiceFilterByDate { get; set; }

  /// <summary>
  /// The maximum number of days between the invoice discount date and the date of the selected bank transaction,
  /// to classify invoice as a match
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(30, typeof (CASetup.daysBeforeInvoiceDiscountDate))]
  [PXUIField(DisplayName = "Days Before Discount Date")]
  public virtual int? DaysBeforeInvoiceDiscountDate { get; set; }

  /// <summary>
  /// The maximum number of days between the date of the selected bank transaction and the invoice due date,
  /// to classify invoice as a match, if bank transaction date earlier than invoice due date
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(30, typeof (CASetup.daysBeforeInvoiceDueDate))]
  [PXUIField(DisplayName = "Days Before Due Date")]
  public virtual int? DaysBeforeInvoiceDueDate { get; set; }

  /// <summary>
  /// The maximum number of days between the invoice due date and the date of the selected bank transaction,
  /// to classify invoice as a match, if bank transaction date later than invoice due date
  /// </summary>
  [PXDBInt(MinValue = 0, MaxValue = 365)]
  [PXDefault(30, typeof (CASetup.daysAfterInvoiceDueDate))]
  [PXUIField(DisplayName = "Days After Due Date")]
  public virtual int? DaysAfterInvoiceDueDate { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that only Invoices with the same cash account or empty cash account should be selected for matching with bank transactions on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false, typeof (CASetup.invoiceFilterByCashAccount))]
  [PXUIField(DisplayName = "Filter by Cash Account")]
  public virtual bool? InvoiceFilterByCashAccount { get; set; }

  /// <summary>
  /// The relative weight of the evaluated difference between the reference numbers of the bank transaction and the invoice.
  /// </summary>
  [PXDBDecimal(MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "87.5", typeof (CASetup.invoiceRefNbrCompareWeight))]
  [PXUIField(DisplayName = "Invoce Nbr. Weight")]
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

  public abstract class receiptTranDaysBefore : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    MatchSettings.receiptTranDaysBefore>
  {
  }

  public abstract class receiptTranDaysAfter : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MatchSettings.receiptTranDaysAfter>
  {
  }

  public abstract class disbursementTranDaysBefore : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MatchSettings.disbursementTranDaysBefore>
  {
  }

  public abstract class disbursementTranDaysAfter : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MatchSettings.disbursementTranDaysAfter>
  {
  }

  public abstract class allowMatchingCreditMemoIncPayments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    MatchSettings.allowMatchingCreditMemoIncPayments>
  {
  }

  public abstract class allowMatchingDebitAdjustment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    MatchSettings.allowMatchingDebitAdjustment>
  {
  }

  public abstract class refNbrCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.refNbrCompareWeight>
  {
  }

  public abstract class dateCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.dateCompareWeight>
  {
  }

  public abstract class payeeCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.payeeCompareWeight>
  {
  }

  public abstract class refNbrComparePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.refNbrComparePercent>
  {
  }

  public abstract class emptyRefNbrMatching : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    MatchSettings.emptyRefNbrMatching>
  {
  }

  public abstract class dateComparePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.dateComparePercent>
  {
  }

  public abstract class payeeComparePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.payeeComparePercent>
  {
  }

  public abstract class dateMeanOffset : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.dateMeanOffset>
  {
  }

  public abstract class dateSigma : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  MatchSettings.dateSigma>
  {
  }

  public abstract class skipVoided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  MatchSettings.skipVoided>
  {
  }

  public abstract class matchThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.matchThreshold>
  {
  }

  public abstract class relativeMatchThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.relativeMatchThreshold>
  {
  }

  public abstract class invoiceFilterByDate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    MatchSettings.invoiceFilterByDate>
  {
  }

  public abstract class daysBeforeInvoiceDiscountDate : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MatchSettings.daysBeforeInvoiceDiscountDate>
  {
  }

  public abstract class daysBeforeInvoiceDueDate : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MatchSettings.daysBeforeInvoiceDueDate>
  {
  }

  public abstract class daysAfterInvoiceDueDate : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    MatchSettings.daysAfterInvoiceDueDate>
  {
  }

  public abstract class invoiceFilterByCashAccount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    MatchSettings.invoiceFilterByCashAccount>
  {
  }

  public abstract class invoiceRefNbrCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.invoiceRefNbrCompareWeight>
  {
  }

  public abstract class invoiceDateCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.invoiceDateCompareWeight>
  {
  }

  public abstract class invoicePayeeCompareWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.invoicePayeeCompareWeight>
  {
  }

  /// <summary>Gets sets InvoiceRefNbrComparePercent</summary>
  public abstract class invoiceRefNbrComparePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.invoiceRefNbrComparePercent>
  {
  }

  /// <summary>Gets sets InvoiceDateComparePercent</summary>
  public abstract class invoiceDateComparePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.invoiceDateComparePercent>
  {
  }

  /// <summary>Gets sets InvoicePayeeComparePercent</summary>
  public abstract class invoicePayeeComparePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.invoicePayeeComparePercent>
  {
  }

  public abstract class averagePaymentDelay : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.averagePaymentDelay>
  {
  }

  public abstract class invoiceDateSigma : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    MatchSettings.invoiceDateSigma>
  {
  }
}
