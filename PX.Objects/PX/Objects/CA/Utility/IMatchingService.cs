// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Utility.IMatchingService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CA.BankStatementHelpers;
using PX.Objects.CA.BankStatementProtoHelpers;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA.Utility;

public interface IMatchingService
{
  IEnumerable<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>> FindDetailMatches<T>(
    T graph,
    CABankTran aDetail,
    IMatchSettings aSettings,
    PX.Objects.CA.BankStatementHelpers.CATranExt[] aBestMatches,
    Decimal aRelevanceTreshold)
    where T : PXGraph, ICABankTransactionsDataProvider;

  Decimal CompareDate(CABankTran aDetail, DateTime? tranDate, double meanValue, double sigma);

  Decimal CompareDate(CABankTran aDetail, CATran aTran, double meanValue, double sigma);

  Decimal CompareDate(
    CABankTran aDetail,
    CABankTranInvoiceMatch aTran,
    double meanValue,
    double sigma);

  Decimal CompareDate(
    CABankTran aDetail,
    CABankTranExpenseDetailMatch aTran,
    double meanValue,
    double sigma);

  Decimal CompareRefNbr(
    CABankTran aDetail,
    string extRefNbr,
    bool looseCompare,
    IMatchSettings matchSettings);

  Decimal CompareRefNbr(
    CABankTran aDetail,
    CATran aTran,
    bool looseCompare,
    IMatchSettings matchSettings);

  Decimal CompareRefNbr(CABankTran aDetail, CABankTranInvoiceMatch aTran, bool looseCompare);

  Decimal CompareRefNbr(
    CABankTran aDetail,
    CABankTranExpenseDetailMatch aTran,
    bool looseCompare,
    IMatchSettings matchSettings);

  Decimal ComparePayee(CABankTran aDetail, CATran aTran);

  Decimal ComparePayee(CABankTran aDetail, CABankTranInvoiceMatch aTran);

  Decimal CompareExpenseReceiptAmount(
    CABankTran aDetail,
    Decimal expenseAmount,
    Decimal diffTreshold);

  Decimal CompareExpenseReceiptAmount(
    CABankTran bankTran,
    CABankTranExpenseDetailMatch receipt,
    IMatchSettings settings);

  Decimal CompareExpenseReceiptAmount(
    Decimal tranAmount,
    Decimal expenseAmount,
    Decimal diffTreshold);

  Decimal EvaluateMatching(string aStr1, string aStr2, bool aCaseSensitive, bool matchEmpty = true);

  Decimal EvaluateMatching(CABankTran aDetail, CATran aTran, IMatchSettings aSettings);

  Decimal EvaluateMatching(
    CABankTran aDetail,
    CABankTranInvoiceMatch aTran,
    IMatchSettings aSettings);

  Decimal EvaluateMatching(
    CABankTran aDetail,
    CABankTranExpenseDetailMatch expenseMath,
    IMatchSettings aSettings);

  Decimal EvaluateTideMatching(string aStr1, string aStr2, bool aCaseSensitive, bool matchEmpty = true);

  Decimal MatchEmptyStrings(string aStr1, string aStr2, bool matchEmpty = true);

  Pair<DateTime, DateTime> GetDateRangeForMatch(CABankTran aDetail, IMatchSettings aSettings);

  Triplet<DateTime, DateTime, DateTime> GetInvoiceDateRangeForMatch(
    CABankTran aDetail,
    IMatchSettings aSettings);

  bool CheckRuleMatches(CABankTran transaction, CABankTranRule rule);
}
