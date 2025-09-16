// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Repositories.ICABankTransactionsRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CA.BankStatementHelpers;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA.Repositories;

public interface ICABankTransactionsRepository
{
  PXResultset<PX.Objects.CA.BankStatementHelpers.CATranExt> SearchForMatchingTransactions(
    PXGraph graph,
    CABankTran aDetail,
    IMatchSettings aSettings,
    Pair<DateTime, DateTime> tranDateRange,
    string curyID,
    bool bestMatchOnly = false);

  PXResultset<CABatch> SearchForMatchingCABatches(
    PXGraph graph,
    CABankTran aDetail,
    IMatchSettings aSettings,
    Pair<DateTime, DateTime> tranDateRange,
    string curyID,
    bool allowUnreleased);

  PXResultset<CABatchDetailOrigDocAggregate> SearchForMatchesInCABatches(
    PXGraph graph,
    string tranType,
    string batchNbr);

  PXResultset<CABankTranMatch> SearchForTranMatchForCABatch(PXGraph graph, string batchNbr);

  PXResult<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARAdjust> FindARInvoiceByInvoiceInfo(
    PXGraph graph,
    CABankTran aRow);

  PXResult<PX.Objects.AP.APInvoice, PX.Objects.AP.APAdjust, PX.Objects.AP.APPayment> FindAPInvoiceByInvoiceInfo(
    PXGraph graph,
    CABankTran aRow);

  object FindInvoiceByInvoiceInfo<T>(T graph, CABankTran aRow, out string Module) where T : PXGraph, ICABankTransactionsDataProvider;

  PXSelectBase<PX.Objects.CA.Light.APInvoice> CreateAPInvoiceQuery(
    PXGraph graph,
    CABankTran aDetail,
    PX.Objects.CA.CashAccount cashAccount,
    Decimal? tranAmount,
    out List<object> bqlParams);

  PXSelectBase<PX.Objects.CA.Light.ARInvoice> CreateARInvoiceQuery(
    PXGraph graph,
    CABankTran aDetail,
    PX.Objects.CA.CashAccount cashAccount,
    Decimal? tranAmount,
    out List<object> bqlParams);
}
