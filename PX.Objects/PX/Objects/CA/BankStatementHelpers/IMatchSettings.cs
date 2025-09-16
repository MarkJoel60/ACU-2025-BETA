// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankStatementHelpers.IMatchSettings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CA.BankStatementHelpers;

public interface IMatchSettings
{
  int? DisbursementTranDaysBefore { get; set; }

  int? DisbursementTranDaysAfter { get; set; }

  int? ReceiptTranDaysBefore { get; set; }

  int? ReceiptTranDaysAfter { get; set; }

  Decimal? RefNbrCompareWeight { get; set; }

  Decimal? DateCompareWeight { get; set; }

  Decimal? PayeeCompareWeight { get; set; }

  Decimal? DateMeanOffset { get; set; }

  Decimal? DateSigma { get; set; }

  bool? SkipVoided { get; set; }

  Decimal? AmountWeight { get; set; }

  Decimal? CuryDiffThreshold { get; set; }

  bool? EmptyRefNbrMatching { get; set; }

  bool? AllowMatchingCreditMemo { get; set; }

  bool? AllowMatchingDebitAdjustment { get; set; }

  Decimal? MatchThreshold { get; set; }

  Decimal? RelativeMatchThreshold { get; set; }

  bool? InvoiceFilterByDate { get; set; }

  int? DaysBeforeInvoiceDiscountDate { get; set; }

  int? DaysBeforeInvoiceDueDate { get; set; }

  int? DaysAfterInvoiceDueDate { get; set; }

  bool? InvoiceFilterByCashAccount { get; set; }

  Decimal? InvoiceRefNbrCompareWeight { get; set; }

  Decimal? InvoiceDateCompareWeight { get; set; }

  Decimal? InvoicePayeeCompareWeight { get; set; }

  Decimal? AveragePaymentDelay { get; set; }

  Decimal? InvoiceDateSigma { get; set; }
}
