// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.ICABankTransactionsDataProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA.BankStatementHelpers;
using System;

#nullable disable
namespace PX.Objects.CA;

public interface ICABankTransactionsDataProvider
{
  PXResultset<PX.Objects.CA.BankStatementHelpers.CATranExt> SearchForMatchingTransactions(
    CABankTran aDetail,
    IMatchSettings aSettings,
    Pair<DateTime, DateTime> tranDateRange,
    string curyID,
    bool bestMatchOnly = false);

  PXResultset<CABatch> SearchForMatchingCABatches(
    CABankTran aDetail,
    IMatchSettings aSettings,
    Pair<DateTime, DateTime> tranDateRange,
    string curyID,
    bool allowUnreleased);

  PXResultset<CABatchDetailOrigDocAggregate> SearchForMatchesInCABatches(
    string tranType,
    string batchNbr);

  bool SkipSearchForMatchesInCABatch(CATran caTran, string batchNbr);

  PXResultset<CABankTranMatch> SearchForTranMatchForCABatch(string batchNbr);

  PXResult<PX.Objects.AR.ARInvoice, ARAdjust> FindARInvoiceByInvoiceInfo(CABankTran aRow);

  PXResult<PX.Objects.AP.APInvoice, APAdjust, PX.Objects.AP.APPayment> FindAPInvoiceByInvoiceInfo(
    CABankTran aRow);

  string GetStatus(CATran tran);

  CashAccount GetCashAccount(int? cashAccountID);
}
