// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Repositories.CABankTransactionsRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA.BankStatementHelpers;
using PX.Objects.CA.Utility;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA.Repositories;

public class CABankTransactionsRepository : ICABankTransactionsRepository
{
  public virtual PXResultset<PX.Objects.CA.BankStatementHelpers.CATranExt> SearchForMatchingTransactions(
    PXGraph graph,
    CABankTran aDetail,
    IMatchSettings aSettings,
    Pair<DateTime, DateTime> tranDateRange,
    string curyID,
    bool bestMatchOnly = false)
  {
    if (!bestMatchOnly || string.IsNullOrEmpty(aDetail.ExtRefNbr) || aDetail.ExtRefNbr.Length <= 4 || this.GetRefNbrCompareWeight(aSettings) <= 0.20M)
    {
      PXSelectReadonly2<PX.Objects.CA.BankStatementHelpers.CATranExt, LeftJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.referenceID>>, LeftJoin<CATran2, On<CATran2.cashAccountID, Equal<CATran.cashAccountID>, And<CATran2.voidedTranID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.tranID>, And<True, Equal<Required<CASetup.skipVoided>>>>>, LeftJoin<CABankTranMatch2, On<CABankTranMatch2.cATranID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.tranID>, And<CABankTranMatch2.tranType, Equal<Required<CABankTran.tranType>>>>, LeftJoin<CABatchDetailOrigDocAggregate, On<CABatchDetailOrigDocAggregate.origModule, Equal<CATran.origModule>, And<CABatchDetailOrigDocAggregate.origDocType, Equal<CATran.origTranType>, And<CABatchDetailOrigDocAggregate.origRefNbr, Equal<CATran.origRefNbr>, And<CATran.isPaymentChargeTran, Equal<False>>>>>, LeftJoin<CABankTranMatch, On<CABankTranMatch.docModule, Equal<BatchModule.moduleAP>, And<CABankTranMatch.docType, Equal<CATranType.cABatch>, And<CABankTranMatch.docRefNbr, Equal<CABatchDetailOrigDocAggregate.batchNbr>, And<CABankTranMatch.tranType, Equal<Required<CABankTran.tranType>>>>>>>>>>>, Where<CATran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CATran.tranDate, Between<Required<CATran.tranDate>, Required<CATran.tranDate>>, And<CATran.curyID, Equal<Required<CATran.curyID>>>>>> pxSelectReadonly2 = new PXSelectReadonly2<PX.Objects.CA.BankStatementHelpers.CATranExt, LeftJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.referenceID>>, LeftJoin<CATran2, On<CATran2.cashAccountID, Equal<CATran.cashAccountID>, And<CATran2.voidedTranID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.tranID>, And<True, Equal<Required<CASetup.skipVoided>>>>>, LeftJoin<CABankTranMatch2, On<CABankTranMatch2.cATranID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.tranID>, And<CABankTranMatch2.tranType, Equal<Required<CABankTran.tranType>>>>, LeftJoin<CABatchDetailOrigDocAggregate, On<CABatchDetailOrigDocAggregate.origModule, Equal<CATran.origModule>, And<CABatchDetailOrigDocAggregate.origDocType, Equal<CATran.origTranType>, And<CABatchDetailOrigDocAggregate.origRefNbr, Equal<CATran.origRefNbr>, And<CATran.isPaymentChargeTran, Equal<False>>>>>, LeftJoin<CABankTranMatch, On<CABankTranMatch.docModule, Equal<BatchModule.moduleAP>, And<CABankTranMatch.docType, Equal<CATranType.cABatch>, And<CABankTranMatch.docRefNbr, Equal<CABatchDetailOrigDocAggregate.batchNbr>, And<CABankTranMatch.tranType, Equal<Required<CABankTran.tranType>>>>>>>>>>>, Where<CATran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CATran.tranDate, Between<Required<CATran.tranDate>, Required<CATran.tranDate>>, And<CATran.curyID, Equal<Required<CATran.curyID>>>>>>(graph);
      bool? nullable = aDetail.MultipleMatchingToPayments;
      if (nullable.GetValueOrDefault())
      {
        nullable = aDetail.MatchReceiptsAndDisbursements;
        if (!nullable.GetValueOrDefault())
        {
          if (aDetail.CuryTranAmt.Value > 0M)
          {
            ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) pxSelectReadonly2).WhereAnd<Where<CATran.curyTranAmt, LessEqual<Required<CATran.curyTranAmt>>, And<CATran.curyTranAmt, GreaterEqual<Zero>>>>();
            goto label_8;
          }
          ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) pxSelectReadonly2).WhereAnd<Where<CATran.curyTranAmt, GreaterEqual<Required<CATran.curyTranAmt>>, And<CATran.curyTranAmt, LessEqual<Zero>>>>();
          goto label_8;
        }
      }
      nullable = aDetail.MatchReceiptsAndDisbursements;
      if (!nullable.GetValueOrDefault())
        ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) pxSelectReadonly2).WhereAnd<Where<CATran.curyTranAmt, Equal<Required<CATran.curyTranAmt>>>>();
label_8:
      nullable = aSettings.SkipVoided;
      if (nullable.GetValueOrDefault())
        ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) pxSelectReadonly2).WhereAnd<Where<CATran.voidedTranID, IsNull, And<CATran2.tranID, IsNull>>>();
      nullable = (graph.Caches[typeof (CASetup)].Current as CASetup).SkipReconciled;
      if (nullable.GetValueOrDefault())
        ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) pxSelectReadonly2).WhereAnd<Where<CATran.reconciled, Equal<False>>>();
      return ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) pxSelectReadonly2).Select(new object[8]
      {
        (object) aSettings.SkipVoided,
        (object) aDetail.TranType,
        (object) aDetail.TranType,
        (object) aDetail.CashAccountID,
        (object) tranDateRange.first,
        (object) tranDateRange.second,
        (object) curyID,
        (object) aDetail.CuryTranAmt.Value
      });
    }
    PXSelectReadonly2<PX.Objects.CA.BankStatementHelpers.CATranExt, LeftJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.referenceID>>, LeftJoin<CATran2, On<CATran2.cashAccountID, Equal<CATran.cashAccountID>, And<CATran2.voidedTranID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.tranID>, And<True, Equal<Required<CASetup.skipVoided>>>>>, LeftJoin<CABankTranMatch2, On<CABankTranMatch2.cATranID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.tranID>, And<CABankTranMatch2.tranType, Equal<Required<CABankTran.tranType>>>>, LeftJoin<CABatchDetailOrigDocAggregate, On<CABatchDetailOrigDocAggregate.origModule, Equal<CATran.origModule>, And<CABatchDetailOrigDocAggregate.origDocType, Equal<CATran.origTranType>, And<CABatchDetailOrigDocAggregate.origRefNbr, Equal<CATran.origRefNbr>, And<CATran.isPaymentChargeTran, Equal<False>>>>>, LeftJoin<CABankTranMatch, On<CABankTranMatch.docModule, Equal<BatchModule.moduleAP>, And<CABankTranMatch.docType, Equal<CATranType.cABatch>, And<CABankTranMatch.docRefNbr, Equal<CABatchDetailOrigDocAggregate.batchNbr>, And<CABankTranMatch.tranType, Equal<Required<CABankTran.tranType>>>>>>>>>>>, Where<CATran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CATran.tranDate, Between<Required<CATran.tranDate>, Required<CATran.tranDate>>, And<CATran.curyID, Equal<Required<CATran.curyID>>, And<CATran.curyTranAmt, Equal<Required<CATran.curyTranAmt>>, And<CATran.extRefNbr, GreaterEqual<Required<CATran.extRefNbr>>>>>>>, OrderBy<Asc<CATran.extRefNbr>>> pxSelectReadonly2_1 = new PXSelectReadonly2<PX.Objects.CA.BankStatementHelpers.CATranExt, LeftJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.referenceID>>, LeftJoin<CATran2, On<CATran2.cashAccountID, Equal<CATran.cashAccountID>, And<CATran2.voidedTranID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.tranID>, And<True, Equal<Required<CASetup.skipVoided>>>>>, LeftJoin<CABankTranMatch2, On<CABankTranMatch2.cATranID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.tranID>, And<CABankTranMatch2.tranType, Equal<Required<CABankTran.tranType>>>>, LeftJoin<CABatchDetailOrigDocAggregate, On<CABatchDetailOrigDocAggregate.origModule, Equal<CATran.origModule>, And<CABatchDetailOrigDocAggregate.origDocType, Equal<CATran.origTranType>, And<CABatchDetailOrigDocAggregate.origRefNbr, Equal<CATran.origRefNbr>, And<CATran.isPaymentChargeTran, Equal<False>>>>>, LeftJoin<CABankTranMatch, On<CABankTranMatch.docModule, Equal<BatchModule.moduleAP>, And<CABankTranMatch.docType, Equal<CATranType.cABatch>, And<CABankTranMatch.docRefNbr, Equal<CABatchDetailOrigDocAggregate.batchNbr>, And<CABankTranMatch.tranType, Equal<Required<CABankTran.tranType>>>>>>>>>>>, Where<CATran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CATran.tranDate, Between<Required<CATran.tranDate>, Required<CATran.tranDate>>, And<CATran.curyID, Equal<Required<CATran.curyID>>, And<CATran.curyTranAmt, Equal<Required<CATran.curyTranAmt>>, And<CATran.extRefNbr, GreaterEqual<Required<CATran.extRefNbr>>>>>>>, OrderBy<Asc<CATran.extRefNbr>>>(graph);
    bool? nullable1 = aSettings.SkipVoided;
    if (nullable1.GetValueOrDefault())
      ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) pxSelectReadonly2_1).WhereAnd<Where<CATran.voidedTranID, IsNull, And<CATran2.tranID, IsNull>>>();
    nullable1 = (graph.Caches[typeof (CASetup)].Current as CASetup).SkipReconciled;
    if (nullable1.GetValueOrDefault())
      ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) pxSelectReadonly2_1).WhereAnd<Where<CATran.reconciled, Equal<False>>>();
    PXResultset<PX.Objects.CA.BankStatementHelpers.CATranExt> pxResultset = new PXResultset<PX.Objects.CA.BankStatementHelpers.CATranExt>();
    foreach (PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt> pxResult in ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) pxSelectReadonly2_1).SelectWindowed(0, 5, new object[9]
    {
      (object) aSettings.SkipVoided,
      (object) aDetail.TranType,
      (object) aDetail.TranType,
      (object) aDetail.CashAccountID,
      (object) tranDateRange.first,
      (object) tranDateRange.second,
      (object) curyID,
      (object) aDetail.CuryTranAmt.Value,
      (object) aDetail.ExtRefNbr?.Trim()
    }))
      pxResultset.Add(pxResult);
    PXSelectReadonly2<PX.Objects.CA.BankStatementHelpers.CATranExt, LeftJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.referenceID>>, LeftJoin<CATran2, On<CATran2.cashAccountID, Equal<CATran.cashAccountID>, And<CATran2.voidedTranID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.tranID>, And<True, Equal<Required<CASetup.skipVoided>>>>>, LeftJoin<CABankTranMatch2, On<CABankTranMatch2.cATranID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.tranID>, And<CABankTranMatch2.tranType, Equal<Required<CABankTran.tranType>>>>, LeftJoin<CABatchDetailOrigDocAggregate, On<CABatchDetailOrigDocAggregate.origModule, Equal<CATran.origModule>, And<CABatchDetailOrigDocAggregate.origDocType, Equal<CATran.origTranType>, And<CABatchDetailOrigDocAggregate.origRefNbr, Equal<CATran.origRefNbr>, And<CATran.isPaymentChargeTran, Equal<False>>>>>, LeftJoin<CABankTranMatch, On<CABankTranMatch.docModule, Equal<BatchModule.moduleAP>, And<CABankTranMatch.docType, Equal<CATranType.cABatch>, And<CABankTranMatch.docRefNbr, Equal<CABatchDetailOrigDocAggregate.batchNbr>, And<CABankTranMatch.tranType, Equal<Required<CABankTran.tranType>>>>>>>>>>>, Where<CATran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CATran.tranDate, Between<Required<CATran.tranDate>, Required<CATran.tranDate>>, And<CATran.curyID, Equal<Required<CATran.curyID>>, And<CATran.curyTranAmt, Equal<Required<CATran.curyTranAmt>>, And<Where<CATran.extRefNbr, Less<Required<CATran.extRefNbr>>, Or<CATran.extRefNbr, IsNull>>>>>>>, OrderBy<Desc<CATran.extRefNbr>>> pxSelectReadonly2_2 = new PXSelectReadonly2<PX.Objects.CA.BankStatementHelpers.CATranExt, LeftJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.referenceID>>, LeftJoin<CATran2, On<CATran2.cashAccountID, Equal<CATran.cashAccountID>, And<CATran2.voidedTranID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.tranID>, And<True, Equal<Required<CASetup.skipVoided>>>>>, LeftJoin<CABankTranMatch2, On<CABankTranMatch2.cATranID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.tranID>, And<CABankTranMatch2.tranType, Equal<Required<CABankTran.tranType>>>>, LeftJoin<CABatchDetailOrigDocAggregate, On<CABatchDetailOrigDocAggregate.origModule, Equal<CATran.origModule>, And<CABatchDetailOrigDocAggregate.origDocType, Equal<CATran.origTranType>, And<CABatchDetailOrigDocAggregate.origRefNbr, Equal<CATran.origRefNbr>, And<CATran.isPaymentChargeTran, Equal<False>>>>>, LeftJoin<CABankTranMatch, On<CABankTranMatch.docModule, Equal<BatchModule.moduleAP>, And<CABankTranMatch.docType, Equal<CATranType.cABatch>, And<CABankTranMatch.docRefNbr, Equal<CABatchDetailOrigDocAggregate.batchNbr>, And<CABankTranMatch.tranType, Equal<Required<CABankTran.tranType>>>>>>>>>>>, Where<CATran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CATran.tranDate, Between<Required<CATran.tranDate>, Required<CATran.tranDate>>, And<CATran.curyID, Equal<Required<CATran.curyID>>, And<CATran.curyTranAmt, Equal<Required<CATran.curyTranAmt>>, And<Where<CATran.extRefNbr, Less<Required<CATran.extRefNbr>>, Or<CATran.extRefNbr, IsNull>>>>>>>, OrderBy<Desc<CATran.extRefNbr>>>(graph);
    bool? nullable2 = aSettings.SkipVoided;
    if (nullable2.GetValueOrDefault())
      ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) pxSelectReadonly2_2).WhereAnd<Where<CATran.voidedTranID, IsNull, And<CATran2.tranID, IsNull>>>();
    nullable2 = (graph.Caches[typeof (CASetup)].Current as CASetup).SkipReconciled;
    if (nullable2.GetValueOrDefault())
      ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) pxSelectReadonly2_2).WhereAnd<Where<CATran.reconciled, Equal<False>>>();
    foreach (PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt> pxResult in ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) pxSelectReadonly2_2).SelectWindowed(0, 5, new object[9]
    {
      (object) aSettings.SkipVoided,
      (object) aDetail.TranType,
      (object) aDetail.TranType,
      (object) aDetail.CashAccountID,
      (object) tranDateRange.first,
      (object) tranDateRange.second,
      (object) curyID,
      (object) aDetail.CuryTranAmt.Value,
      (object) aDetail.ExtRefNbr?.Trim()
    }))
      pxResultset.Add(pxResult);
    return pxResultset;
  }

  public virtual PXResultset<CABatch> SearchForMatchingCABatches(
    PXGraph graph,
    CABankTran aDetail,
    IMatchSettings aSettings,
    Pair<DateTime, DateTime> tranDateRange,
    string curyID,
    bool allowUnreleased)
  {
    PXSelectJoin<CABatch, LeftJoin<CABatchDetail, On<CABatchDetail.batchNbr, Equal<CABatch.batchNbr>>, LeftJoin<PX.Objects.CA.Light.APPayment, On<PX.Objects.CA.Light.APPayment.docType, Equal<CABatchDetail.origDocType>, And<PX.Objects.CA.Light.APPayment.refNbr, Equal<CABatchDetail.origRefNbr>>>, LeftJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.Light.APPayment.vendorID>>, LeftJoin<CABankTranMatch, On<CABankTranMatch.cATranID, Equal<PX.Objects.CA.Light.APPayment.cATranID>, And<CABankTranMatch.tranType, Equal<Required<CABankTran.tranType>>>>>>>>, Where<CABatch.cashAccountID, Equal<Required<CABatch.cashAccountID>>, And2<Where<CABatch.released, Equal<True>, Or<Required<CASetup.allowMatchingToUnreleasedBatch>, Equal<True>>>, And<Where<CABatch.tranDate, Between<Required<CABatch.tranDate>, Required<CABatch.tranDate>>, And<CABatch.curyID, Equal<Required<CABatch.curyID>>, And<Where<CABatch.reconciled, Equal<False>, Or<Required<CASetup.skipReconciled>, Equal<False>>>>>>>>>> pxSelectJoin = new PXSelectJoin<CABatch, LeftJoin<CABatchDetail, On<CABatchDetail.batchNbr, Equal<CABatch.batchNbr>>, LeftJoin<PX.Objects.CA.Light.APPayment, On<PX.Objects.CA.Light.APPayment.docType, Equal<CABatchDetail.origDocType>, And<PX.Objects.CA.Light.APPayment.refNbr, Equal<CABatchDetail.origRefNbr>>>, LeftJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.Light.APPayment.vendorID>>, LeftJoin<CABankTranMatch, On<CABankTranMatch.cATranID, Equal<PX.Objects.CA.Light.APPayment.cATranID>, And<CABankTranMatch.tranType, Equal<Required<CABankTran.tranType>>>>>>>>, Where<CABatch.cashAccountID, Equal<Required<CABatch.cashAccountID>>, And2<Where<CABatch.released, Equal<True>, Or<Required<CASetup.allowMatchingToUnreleasedBatch>, Equal<True>>>, And<Where<CABatch.tranDate, Between<Required<CABatch.tranDate>, Required<CABatch.tranDate>>, And<CABatch.curyID, Equal<Required<CABatch.curyID>>, And<Where<CABatch.reconciled, Equal<False>, Or<Required<CASetup.skipReconciled>, Equal<False>>>>>>>>>>(graph);
    if (aDetail.MultipleMatchingToPayments.GetValueOrDefault() && !aDetail.MatchReceiptsAndDisbursements.GetValueOrDefault())
      ((PXSelectBase<CABatch>) pxSelectJoin).WhereAnd<Where<CABatch.curyDetailTotal, LessEqual<Required<CABatch.curyDetailTotal>>>>();
    else if (!aDetail.MatchReceiptsAndDisbursements.GetValueOrDefault())
      ((PXSelectBase<CABatch>) pxSelectJoin).WhereAnd<Where<CABatch.curyDetailTotal, Equal<Required<CABatch.curyDetailTotal>>>>();
    if (aSettings.SkipVoided.GetValueOrDefault())
      ((PXSelectBase<CABatch>) pxSelectJoin).WhereAnd<Where<CABatch.voided, NotEqual<True>>>();
    return ((PXSelectBase<CABatch>) pxSelectJoin).Select(new object[8]
    {
      (object) aDetail.TranType,
      (object) aDetail.CashAccountID,
      (object) allowUnreleased,
      (object) tranDateRange.first,
      (object) tranDateRange.second,
      (object) curyID,
      (object) (graph.Caches[typeof (CASetup)].Current as CASetup).SkipReconciled.GetValueOrDefault(),
      (object) (-1M * aDetail.CuryTranAmt.Value)
    });
  }

  public virtual PXResultset<CABatchDetailOrigDocAggregate> SearchForMatchesInCABatches(
    PXGraph graph,
    string tranType,
    string batchNbr)
  {
    return PXSelectBase<CABatchDetailOrigDocAggregate, PXSelectJoin<CABatchDetailOrigDocAggregate, InnerJoin<CATran, On<CATran.origModule, Equal<CABatchDetailOrigDocAggregate.origModule>, And<CATran.origTranType, Equal<CABatchDetailOrigDocAggregate.origDocType>, And<CATran.origRefNbr, Equal<CABatchDetailOrigDocAggregate.origRefNbr>, And<CATran.isPaymentChargeTran, Equal<False>>>>>, InnerJoin<CABankTranMatch, On<CABankTranMatch.cATranID, Equal<CATran.tranID>, And<CABankTranMatch.tranType, Equal<Required<CABankTran.tranType>>>>>>, Where<CABatchDetailOrigDocAggregate.batchNbr, Equal<Required<CABatch.batchNbr>>>>.Config>.Select(graph, new object[2]
    {
      (object) tranType,
      (object) batchNbr
    });
  }

  public virtual PXResultset<CABankTranMatch> SearchForTranMatchForCABatch(
    PXGraph graph,
    string batchNbr)
  {
    return PXSelectBase<CABankTranMatch, PXSelect<CABankTranMatch, Where<CABankTranMatch.docRefNbr, Equal<Required<CABankTranMatch.docRefNbr>>, And<CABankTranMatch.docType, Equal<CATranType.cABatch>, And<CABankTranMatch.docModule, Equal<BatchModule.moduleAP>>>>>.Config>.Select(graph, new object[1]
    {
      (object) batchNbr
    });
  }

  public virtual PXResult<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARAdjust> FindARInvoiceByInvoiceInfo(
    PXGraph graph,
    CABankTran aRow)
  {
    PXResult<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARAdjust> invoiceByInvoiceInfo = (PXResult<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARAdjust>) PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelectJoin<PX.Objects.AR.ARInvoice, LeftJoin<PX.Objects.AR.ARAdjust, On<PX.Objects.AR.ARAdjust.adjdDocType, Equal<PX.Objects.AR.ARInvoice.docType>, And<PX.Objects.AR.ARAdjust.adjdRefNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>, And<PX.Objects.AR.ARAdjust.released, Equal<boolFalse>>>>>, Where<PX.Objects.AR.ARInvoice.docType, Equal<ARDocType.invoice>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.Select(graph, new object[1]
    {
      (object) aRow.InvoiceInfo
    }));
    if (invoiceByInvoiceInfo == null)
      invoiceByInvoiceInfo = (PXResult<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARAdjust>) PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelectJoin<PX.Objects.AR.ARInvoice, LeftJoin<PX.Objects.AR.ARAdjust, On<PX.Objects.AR.ARAdjust.adjdDocType, Equal<PX.Objects.AR.ARInvoice.docType>, And<PX.Objects.AR.ARAdjust.adjdRefNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>, And<PX.Objects.AR.ARAdjust.released, Equal<boolFalse>>>>>, Where<PX.Objects.AR.ARInvoice.docType, Equal<ARDocType.invoice>, And<PX.Objects.AR.ARInvoice.invoiceNbr, Equal<Required<PX.Objects.AR.ARInvoice.invoiceNbr>>>>>.Config>.Select(graph, new object[1]
      {
        (object) aRow.InvoiceInfo
      }));
    return invoiceByInvoiceInfo;
  }

  public virtual PXResult<PX.Objects.AP.APInvoice, PX.Objects.AP.APAdjust, PX.Objects.AP.APPayment> FindAPInvoiceByInvoiceInfo(
    PXGraph graph,
    CABankTran aRow)
  {
    PXResult<PX.Objects.AP.APInvoice, PX.Objects.AP.APAdjust, PX.Objects.AP.APPayment> invoiceByInvoiceInfo = (PXResult<PX.Objects.AP.APInvoice, PX.Objects.AP.APAdjust, PX.Objects.AP.APPayment>) PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(PXSelectBase<PX.Objects.AP.APInvoice, PXSelectJoin<PX.Objects.AP.APInvoice, LeftJoin<PX.Objects.AP.APAdjust, On<PX.Objects.AP.APAdjust.adjdDocType, Equal<PX.Objects.AP.APInvoice.docType>, And<PX.Objects.AP.APAdjust.adjdRefNbr, Equal<PX.Objects.AP.APInvoice.refNbr>, And<PX.Objects.AP.APAdjust.released, Equal<boolFalse>>>>, LeftJoin<PX.Objects.AP.APPayment, On<PX.Objects.AP.APPayment.docType, Equal<PX.Objects.AP.APInvoice.docType>, And<PX.Objects.AP.APPayment.refNbr, Equal<PX.Objects.AP.APInvoice.refNbr>, And<Where<PX.Objects.AP.APPayment.docType, Equal<APDocType.prepayment>, Or<PX.Objects.AP.APPayment.docType, Equal<APDocType.debitAdj>>>>>>>>, Where<PX.Objects.AP.APInvoice.docType, Equal<APDocType.invoice>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>>.Config>.Select(graph, new object[1]
    {
      (object) aRow.InvoiceInfo
    }));
    if (invoiceByInvoiceInfo == null)
      invoiceByInvoiceInfo = (PXResult<PX.Objects.AP.APInvoice, PX.Objects.AP.APAdjust, PX.Objects.AP.APPayment>) PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(PXSelectBase<PX.Objects.AP.APInvoice, PXSelectJoin<PX.Objects.AP.APInvoice, LeftJoin<PX.Objects.AP.APAdjust, On<PX.Objects.AP.APAdjust.adjdDocType, Equal<PX.Objects.AP.APInvoice.docType>, And<PX.Objects.AP.APAdjust.adjdRefNbr, Equal<PX.Objects.AP.APInvoice.refNbr>, And<PX.Objects.AP.APAdjust.released, Equal<boolFalse>>>>, LeftJoin<PX.Objects.AP.APPayment, On<PX.Objects.AP.APPayment.docType, Equal<PX.Objects.AP.APInvoice.docType>, And<PX.Objects.AP.APPayment.refNbr, Equal<PX.Objects.AP.APInvoice.refNbr>, And<Where<PX.Objects.AP.APPayment.docType, Equal<APDocType.prepayment>, Or<PX.Objects.AP.APPayment.docType, Equal<APDocType.debitAdj>>>>>>>>, Where<PX.Objects.AP.APInvoice.docType, Equal<APDocType.invoice>, And<PX.Objects.AP.APInvoice.invoiceNbr, Equal<Required<PX.Objects.AP.APInvoice.invoiceNbr>>>>>.Config>.Select(graph, new object[1]
      {
        (object) aRow.InvoiceInfo
      }));
    return invoiceByInvoiceInfo;
  }

  public virtual PXSelectBase<PX.Objects.CA.Light.APInvoice> CreateAPInvoiceQuery(
    PXGraph graph,
    CABankTran aDetail,
    PX.Objects.CA.CashAccount cashAccount,
    Decimal? tranAmount,
    out List<object> bqlParams)
  {
    bqlParams = new List<object>()
    {
      (object) aDetail.TranType,
      (object) cashAccount.CuryID,
      (object) aDetail.TranDate,
      (object) aDetail.TranPeriodID
    };
    IMatchSettings aSettings = (IMatchSettings) cashAccount;
    IMatchingService service = graph.GetService<IMatchingService>();
    PXSelectBase<PX.Objects.CA.Light.APInvoice> apInvoiceQuery = (PXSelectBase<PX.Objects.CA.Light.APInvoice>) new PXSelectJoin<PX.Objects.CA.Light.APInvoice, LeftJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.Light.APInvoice.vendorID>>, LeftJoin<CABankTranMatch, On<CABankTranMatch.docModule, Equal<BatchModule.moduleAP>, And<CABankTranMatch.docType, Equal<PX.Objects.CA.Light.APInvoice.docType>, And<CABankTranMatch.docRefNbr, Equal<PX.Objects.CA.Light.APInvoice.refNbr>, And<CABankTranMatch.tranType, Equal<Required<CABankTranMatch.tranType>>>>>>, LeftJoin<PX.Objects.CA.Light.APAdjust, On<PX.Objects.CA.Light.APAdjust.adjdDocType, Equal<PX.Objects.CA.Light.APInvoice.docType>, And<PX.Objects.CA.Light.APAdjust.adjdRefNbr, Equal<PX.Objects.CA.Light.APInvoice.refNbr>, And<PX.Objects.CA.Light.APAdjust.released, Equal<boolFalse>>>>, LeftJoin<PX.Objects.CA.Light.APPayment, On<PX.Objects.CA.Light.APPayment.docType, Equal<PX.Objects.CA.Light.APInvoice.docType>, And<PX.Objects.CA.Light.APPayment.refNbr, Equal<PX.Objects.CA.Light.APInvoice.refNbr>>>, LeftJoin<PX.Objects.CA.Light.CABankTranAdjustment, On<PX.Objects.CA.Light.CABankTranAdjustment.adjdDocType, Equal<PX.Objects.CA.Light.APInvoice.docType>, And<PX.Objects.CA.Light.CABankTranAdjustment.adjdRefNbr, Equal<PX.Objects.CA.Light.APInvoice.refNbr>, And<PX.Objects.CA.Light.CABankTranAdjustment.adjdModule, Equal<BatchModule.moduleAP>, And<PX.Objects.CA.Light.CABankTranAdjustment.released, Equal<boolFalse>>>>>, LeftJoin<PX.Objects.CA.Light.Branch, On<PX.Objects.CA.Light.Branch.branchID, Equal<PX.Objects.CA.Light.APRegister.branchID>>>>>>>>, Where<PX.Objects.CA.Light.APAdjust.adjgRefNbr, IsNull, And<PX.Objects.CA.Light.Branch.active, Equal<True>, And<PX.Objects.CA.Light.APInvoice.openDoc, Equal<True>, And<PX.Objects.CA.Light.APInvoice.released, Equal<True>, And<PX.Objects.CA.Light.APInvoice.curyDocBal, NotEqual<decimal0>, And<PX.Objects.CA.Light.APInvoice.curyID, Equal<Required<PX.Objects.AP.APInvoice.curyID>>, And<Where<PX.Objects.CA.Light.APInvoice.docDate, LessEqual<Required<PX.Objects.CA.Light.APInvoice.docDate>>, And<PX.Objects.CA.Light.APRegister.finPeriodID, LessEqual<Required<PX.Objects.CA.Light.APRegister.finPeriodID>>, Or<Current<APSetup.earlyChecks>, Equal<True>>>>>>>>>>>>(graph);
    if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAP>())
      apInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.APRegister.docType, Equal<APDocType.prepaymentInvoice>, And<PX.Objects.CA.Light.APRegister.pendingPayment, Equal<True>, Or<PX.Objects.CA.Light.APRegister.docType, NotEqual<APDocType.prepaymentInvoice>>>>>();
    bool? nullable;
    if (aDetail.PayeeBAccountID.HasValue)
    {
      nullable = aDetail.MultipleMatching;
      bool flag = false;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      {
        if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAP>())
        {
          apInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.APInvoice.docType, In3<APDocType.invoice, APDocType.debitAdj, APDocType.prepaymentInvoice, APDocType.creditAdj>>>();
          goto label_12;
        }
        apInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.APInvoice.docType, In3<APDocType.invoice, APDocType.debitAdj, APDocType.creditAdj>>>();
        goto label_12;
      }
    }
    if (aDetail.DrCr == "C")
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAP>())
        apInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.APInvoice.docType, Equal<APDocType.invoice>, Or<PX.Objects.CA.Light.APInvoice.docType, Equal<APDocType.prepaymentInvoice>>>>();
      else
        apInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.APInvoice.docType, Equal<APDocType.invoice>, And<PX.Objects.CA.Light.APPayment.refNbr, IsNull>>>();
    }
    else
      apInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.APInvoice.docType, Equal<APDocType.debitAdj>>>();
label_12:
    nullable = aDetail.MultipleMatching;
    if (nullable.GetValueOrDefault())
    {
      bool flag = aDetail.PayeeBAccountID.HasValue || aDetail.ChargeTypeID != null && !(aDetail.DrCr == aDetail.ChargeDrCr) && aDetail.ChargeDrCr != null;
      nullable = aSettings.InvoiceFilterByDate;
      if (nullable.GetValueOrDefault())
      {
        Triplet<DateTime, DateTime, DateTime> dateRangeForMatch = service.GetInvoiceDateRangeForMatch(aDetail, aSettings);
        if (!flag)
        {
          apInvoiceQuery.WhereAnd<Where2<Where2<Where<PX.Objects.CA.Light.APInvoice.discDate, Less<Required<PX.Objects.CA.Light.APInvoice.discDate>>, Or<PX.Objects.CA.Light.APInvoice.discDate, IsNull>>, And<PX.Objects.CA.Light.APInvoice.curyDocBal, LessEqual<Required<PX.Objects.CA.Light.APInvoice.curyDocBal>>, And<Where<PX.Objects.CA.Light.APInvoice.dueDate, GreaterEqual<Required<PX.Objects.CA.Light.APInvoice.dueDate>>, And<PX.Objects.CA.Light.APInvoice.dueDate, LessEqual<Required<PX.Objects.CA.Light.APInvoice.dueDate>>, Or<PX.Objects.CA.Light.APInvoice.dueDate, IsNull>>>>>>, Or<Where<PX.Objects.CA.Light.APInvoice.discDate, GreaterEqual<Required<PX.Objects.CA.Light.APInvoice.discDate>>, And<Sub<PX.Objects.CA.Light.APInvoice.curyDocBal, PX.Objects.CA.Light.APInvoice.curyDiscBal>, LessEqual<Required<PX.Objects.CA.Light.APInvoice.curyDocBal>>, And<PX.Objects.CA.Light.APInvoice.discDate, LessEqual<Required<PX.Objects.CA.Light.APInvoice.discDate>>>>>>>>();
          bqlParams.AddRange((IEnumerable<object>) new object[7]
          {
            (object) aDetail.TranDate,
            (object) tranAmount,
            (object) dateRangeForMatch.first,
            (object) dateRangeForMatch.second,
            (object) aDetail.TranDate,
            (object) tranAmount,
            (object) dateRangeForMatch.third
          });
        }
        else
        {
          apInvoiceQuery.WhereAnd<Where2<Where<PX.Objects.CA.Light.APInvoice.dueDate, GreaterEqual<Required<PX.Objects.CA.Light.APInvoice.dueDate>>, And<PX.Objects.CA.Light.APInvoice.dueDate, LessEqual<Required<PX.Objects.CA.Light.APInvoice.dueDate>>, Or<PX.Objects.CA.Light.APInvoice.dueDate, IsNull>>>, Or<Where<PX.Objects.CA.Light.APInvoice.discDate, GreaterEqual<Required<PX.Objects.CA.Light.APInvoice.discDate>>, And<PX.Objects.CA.Light.APInvoice.discDate, LessEqual<Required<PX.Objects.CA.Light.APInvoice.discDate>>>>>>>();
          bqlParams.AddRange((IEnumerable<object>) new object[4]
          {
            (object) dateRangeForMatch.first,
            (object) dateRangeForMatch.second,
            (object) aDetail.TranDate,
            (object) dateRangeForMatch.third
          });
        }
      }
      else if (!flag)
      {
        apInvoiceQuery.WhereAnd<Where2<Where2<Where<PX.Objects.CA.Light.APInvoice.discDate, Less<Required<PX.Objects.CA.Light.APInvoice.discDate>>, Or<PX.Objects.CA.Light.APInvoice.discDate, IsNull>>, And<PX.Objects.CA.Light.APInvoice.curyDocBal, LessEqual<Required<PX.Objects.CA.Light.APInvoice.curyDocBal>>>>, Or<Where<PX.Objects.CA.Light.APInvoice.discDate, GreaterEqual<Required<PX.Objects.CA.Light.APInvoice.discDate>>, And<Sub<PX.Objects.CA.Light.APInvoice.curyDocBal, PX.Objects.CA.Light.APInvoice.curyDiscBal>, LessEqual<Required<PX.Objects.CA.Light.APInvoice.curyDocBal>>>>>>>();
        bqlParams.AddRange((IEnumerable<object>) new object[4]
        {
          (object) aDetail.TranDate,
          (object) tranAmount,
          (object) aDetail.TranDate,
          (object) tranAmount
        });
      }
    }
    else
    {
      nullable = aSettings.InvoiceFilterByDate;
      if (nullable.GetValueOrDefault())
      {
        Triplet<DateTime, DateTime, DateTime> dateRangeForMatch = service.GetInvoiceDateRangeForMatch(aDetail, aSettings);
        apInvoiceQuery.WhereAnd<Where2<Where2<Where<PX.Objects.CA.Light.APInvoice.discDate, Less<Required<PX.Objects.CA.Light.APInvoice.discDate>>, Or<PX.Objects.CA.Light.APInvoice.discDate, IsNull>>, And<PX.Objects.CA.Light.APInvoice.curyDocBal, Equal<Required<PX.Objects.CA.Light.APInvoice.curyDocBal>>, And<Where<PX.Objects.CA.Light.APInvoice.dueDate, GreaterEqual<Required<PX.Objects.CA.Light.APInvoice.dueDate>>, And<PX.Objects.CA.Light.APInvoice.dueDate, LessEqual<Required<PX.Objects.CA.Light.APInvoice.dueDate>>, Or<PX.Objects.CA.Light.APInvoice.dueDate, IsNull>>>>>>, Or<Where<PX.Objects.CA.Light.APInvoice.discDate, GreaterEqual<Required<PX.Objects.CA.Light.APInvoice.discDate>>, And<Sub<PX.Objects.CA.Light.APInvoice.curyDocBal, PX.Objects.CA.Light.APInvoice.curyDiscBal>, Equal<Required<PX.Objects.CA.Light.APInvoice.curyDocBal>>, And<PX.Objects.CA.Light.APInvoice.discDate, LessEqual<Required<PX.Objects.AP.APInvoice.discDate>>>>>>>>();
        bqlParams.AddRange((IEnumerable<object>) new object[7]
        {
          (object) aDetail.TranDate,
          (object) tranAmount,
          (object) dateRangeForMatch.first,
          (object) dateRangeForMatch.second,
          (object) aDetail.TranDate,
          (object) tranAmount,
          (object) dateRangeForMatch.third
        });
      }
      else
      {
        apInvoiceQuery.WhereAnd<Where2<Where2<Where<PX.Objects.CA.Light.APInvoice.discDate, Less<Required<PX.Objects.CA.Light.APInvoice.discDate>>, Or<PX.Objects.CA.Light.APInvoice.discDate, IsNull>>, And<PX.Objects.CA.Light.APInvoice.curyDocBal, Equal<Required<PX.Objects.CA.Light.APInvoice.curyDocBal>>>>, Or<Where<PX.Objects.CA.Light.APInvoice.discDate, GreaterEqual<Required<PX.Objects.CA.Light.APInvoice.discDate>>, And<Sub<PX.Objects.CA.Light.APInvoice.curyDocBal, PX.Objects.CA.Light.APInvoice.curyDiscBal>, Equal<Required<PX.Objects.CA.Light.APInvoice.curyDocBal>>>>>>>();
        bqlParams.AddRange((IEnumerable<object>) new object[4]
        {
          (object) aDetail.TranDate,
          (object) tranAmount,
          (object) aDetail.TranDate,
          (object) tranAmount
        });
      }
    }
    if (aDetail.PayeeBAccountID.HasValue)
    {
      apInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.APInvoice.vendorID, Equal<Required<PX.Objects.AP.APInvoice.vendorID>>>>();
      bqlParams.Add((object) aDetail.PayeeBAccountID);
    }
    PX.Objects.CA.CashAccount cashAccount1 = (PX.Objects.CA.CashAccount) null;
    nullable = aSettings.InvoiceFilterByCashAccount;
    if (nullable.GetValueOrDefault())
    {
      cashAccount1 = PX.Objects.CA.CashAccount.PK.Find(graph, aDetail.CashAccountID);
      apInvoiceQuery.WhereAnd<Where<PX.Objects.AP.APInvoice.payAccountID, Equal<Required<PX.Objects.CA.Light.APInvoice.payAccountID>>, Or<Where<PX.Objects.CA.Light.APInvoice.payAccountID, IsNull, And<PX.Objects.CA.Light.APRegister.branchID, Equal<Required<PX.Objects.CA.Light.APRegister.branchID>>>>>>>();
      bqlParams.Add((object) aDetail.CashAccountID);
      bqlParams.Add((object) cashAccount1.BranchID);
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.interBranch>())
    {
      int[] childBranchIds = PXAccess.GetChildBranchIDs(PXAccess.GetParentOrganizationID((cashAccount1 ?? PX.Objects.CA.CashAccount.PK.Find(graph, aDetail.CashAccountID)).BranchID), true);
      apInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.APRegister.branchID, In<Required<PX.Objects.CA.Light.APRegister.branchID>>>>();
      bqlParams.Add((object) childBranchIds);
    }
    return apInvoiceQuery;
  }

  public virtual PXSelectBase<PX.Objects.CA.Light.ARInvoice> CreateARInvoiceQuery(
    PXGraph graph,
    CABankTran aDetail,
    PX.Objects.CA.CashAccount cashAccount,
    Decimal? tranAmount,
    out List<object> bqlParams)
  {
    bqlParams = new List<object>()
    {
      (object) aDetail.TranType,
      (object) cashAccount.CuryID
    };
    IMatchSettings aSettings = (IMatchSettings) cashAccount;
    IMatchingService service = graph.GetService<IMatchingService>();
    PXSelectBase<PX.Objects.CA.Light.ARInvoice> arInvoiceQuery = (PXSelectBase<PX.Objects.CA.Light.ARInvoice>) new PXSelectJoin<PX.Objects.CA.Light.ARInvoice, LeftJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.Light.ARInvoice.customerID>>, LeftJoin<CABankTranMatch, On<CABankTranMatch.docModule, Equal<BatchModule.moduleAR>, And<CABankTranMatch.docType, Equal<PX.Objects.CA.Light.ARInvoice.docType>, And<CABankTranMatch.docRefNbr, Equal<PX.Objects.CA.Light.ARInvoice.refNbr>, And<CABankTranMatch.tranType, Equal<Required<CABankTranMatch.tranType>>>>>>, LeftJoin<PX.Objects.CA.Light.ARAdjust, On<PX.Objects.CA.Light.ARAdjust.adjdDocType, Equal<PX.Objects.CA.Light.ARInvoice.docType>, And<PX.Objects.CA.Light.ARAdjust.adjdRefNbr, Equal<PX.Objects.CA.Light.ARInvoice.refNbr>, And<PX.Objects.CA.Light.ARAdjust.released, Equal<boolFalse>, And<PX.Objects.CA.Light.ARAdjust.voided, Equal<boolFalse>>>>>, LeftJoin<PX.Objects.CA.Light.CABankTranAdjustment, On<PX.Objects.CA.Light.CABankTranAdjustment.adjdDocType, Equal<PX.Objects.CA.Light.ARInvoice.docType>, And<PX.Objects.CA.Light.CABankTranAdjustment.adjdRefNbr, Equal<PX.Objects.CA.Light.ARInvoice.refNbr>, And<PX.Objects.CA.Light.CABankTranAdjustment.adjdModule, Equal<BatchModule.moduleAR>, And<PX.Objects.CA.Light.CABankTranAdjustment.released, Equal<boolFalse>>>>>, LeftJoin<PX.Objects.CA.Light.Branch, On<PX.Objects.CA.Light.Branch.branchID, Equal<PX.Objects.CA.Light.ARRegister.branchID>>>>>>>, Where<PX.Objects.CA.Light.ARAdjust.adjgRefNbr, IsNull, And<PX.Objects.CA.Light.Branch.active, Equal<True>, And<PX.Objects.CA.Light.ARInvoice.openDoc, Equal<True>, And<PX.Objects.CA.Light.ARInvoice.released, Equal<True>, And<PX.Objects.CA.Light.ARInvoice.curyDocBal, NotEqual<decimal0>, And<PX.Objects.CA.Light.ARInvoice.curyID, Equal<Required<PX.Objects.AR.ARInvoice.curyID>>>>>>>>>(graph);
    if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>())
      arInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.ARRegister.docType, Equal<ARDocType.prepaymentInvoice>, And<PX.Objects.CA.Light.ARRegister.pendingPayment, Equal<True>, Or<PX.Objects.CA.Light.ARRegister.docType, NotEqual<ARDocType.prepaymentInvoice>>>>>();
    if (aDetail.PayeeBAccountID.HasValue)
    {
      bool? multipleMatching = aDetail.MultipleMatching;
      bool flag = false;
      if (!(multipleMatching.GetValueOrDefault() == flag & multipleMatching.HasValue))
      {
        if (aDetail.DrCr == "D")
        {
          if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>())
          {
            arInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.invoice>, Or<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.debitMemo>, Or<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.finCharge>, Or<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.prepaymentInvoice>, Or<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.creditMemo>>>>>>>();
            goto label_14;
          }
          arInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.invoice>, Or<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.debitMemo>, Or<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.finCharge>, Or<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.creditMemo>>>>>>();
          goto label_14;
        }
        arInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.creditMemo>>>();
        goto label_14;
      }
    }
    if (aDetail.DrCr == "D")
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>())
        arInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.invoice>, Or<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.prepaymentInvoice>>>>();
      else
        arInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.invoice>>>();
    }
    else
      arInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.ARInvoice.docType, Equal<ARDocType.creditMemo>>>();
label_14:
    if (aDetail.MultipleMatching.GetValueOrDefault())
    {
      bool flag = aDetail.PayeeBAccountID.HasValue || aDetail.ChargeTypeID != null && !(aDetail.DrCr == aDetail.ChargeDrCr) && aDetail.ChargeDrCr != null;
      if (aSettings.InvoiceFilterByDate.GetValueOrDefault())
      {
        Triplet<DateTime, DateTime, DateTime> dateRangeForMatch = service.GetInvoiceDateRangeForMatch(aDetail, aSettings);
        if (!flag)
        {
          arInvoiceQuery.WhereAnd<Where2<Where2<Where<PX.Objects.CA.Light.ARInvoice.discDate, Less<Required<PX.Objects.CA.Light.ARInvoice.discDate>>, Or<PX.Objects.CA.Light.ARInvoice.discDate, IsNull>>, And<PX.Objects.CA.Light.ARInvoice.curyDocBal, LessEqual<Required<PX.Objects.AR.ARInvoice.curyDocBal>>, And<Where<PX.Objects.CA.Light.ARRegister.dueDate, GreaterEqual<Required<PX.Objects.AR.ARInvoice.dueDate>>, And<PX.Objects.CA.Light.ARRegister.dueDate, LessEqual<Required<PX.Objects.AR.ARInvoice.dueDate>>, Or<PX.Objects.CA.Light.ARRegister.dueDate, IsNull>>>>>>, Or<Where<PX.Objects.CA.Light.ARInvoice.discDate, GreaterEqual<Required<PX.Objects.CA.Light.ARInvoice.discDate>>, And<Sub<PX.Objects.CA.Light.ARInvoice.curyDocBal, PX.Objects.CA.Light.ARInvoice.curyDiscBal>, LessEqual<Required<PX.Objects.AR.ARInvoice.curyDocBal>>, And<PX.Objects.CA.Light.ARInvoice.discDate, LessEqual<Required<PX.Objects.AR.ARInvoice.discDate>>>>>>>>();
          bqlParams.AddRange((IEnumerable<object>) new object[7]
          {
            (object) aDetail.TranDate,
            (object) tranAmount,
            (object) dateRangeForMatch.first,
            (object) dateRangeForMatch.second,
            (object) aDetail.TranDate,
            (object) tranAmount,
            (object) dateRangeForMatch.third
          });
        }
        else
        {
          arInvoiceQuery.WhereAnd<Where2<Where<PX.Objects.CA.Light.ARRegister.dueDate, GreaterEqual<Required<PX.Objects.AR.ARInvoice.dueDate>>, And<PX.Objects.CA.Light.ARRegister.dueDate, LessEqual<Required<PX.Objects.AR.ARInvoice.dueDate>>, Or<PX.Objects.CA.Light.ARRegister.dueDate, IsNull>>>, Or<Where<PX.Objects.CA.Light.ARInvoice.discDate, GreaterEqual<Required<PX.Objects.CA.Light.ARInvoice.discDate>>, And<PX.Objects.CA.Light.ARInvoice.discDate, LessEqual<Required<PX.Objects.AR.ARInvoice.discDate>>>>>>>();
          bqlParams.AddRange((IEnumerable<object>) new object[4]
          {
            (object) dateRangeForMatch.first,
            (object) dateRangeForMatch.second,
            (object) aDetail.TranDate,
            (object) dateRangeForMatch.third
          });
        }
      }
      else if (!flag)
      {
        arInvoiceQuery.WhereAnd<Where2<Where2<Where<PX.Objects.CA.Light.ARInvoice.discDate, Less<Required<PX.Objects.CA.Light.ARInvoice.discDate>>, Or<PX.Objects.CA.Light.ARInvoice.discDate, IsNull>>, And<PX.Objects.CA.Light.ARInvoice.curyDocBal, LessEqual<Required<PX.Objects.AR.ARInvoice.curyDocBal>>>>, Or<Where<PX.Objects.CA.Light.ARInvoice.discDate, GreaterEqual<Required<PX.Objects.CA.Light.ARInvoice.discDate>>, And<Sub<PX.Objects.CA.Light.ARInvoice.curyDocBal, PX.Objects.CA.Light.ARInvoice.curyDiscBal>, LessEqual<Required<PX.Objects.AR.ARInvoice.curyDocBal>>>>>>>();
        bqlParams.AddRange((IEnumerable<object>) new object[4]
        {
          (object) aDetail.TranDate,
          (object) tranAmount,
          (object) aDetail.TranDate,
          (object) tranAmount
        });
      }
    }
    else if (aSettings.InvoiceFilterByDate.GetValueOrDefault())
    {
      Triplet<DateTime, DateTime, DateTime> dateRangeForMatch = service.GetInvoiceDateRangeForMatch(aDetail, aSettings);
      arInvoiceQuery.WhereAnd<Where2<Where2<Where<PX.Objects.CA.Light.ARInvoice.discDate, Less<Required<PX.Objects.AR.ARInvoice.discDate>>, Or<PX.Objects.CA.Light.ARInvoice.discDate, IsNull>>, And<PX.Objects.CA.Light.ARInvoice.curyDocBal, Equal<Required<PX.Objects.AR.ARInvoice.curyDocBal>>, And<Where<PX.Objects.CA.Light.ARRegister.dueDate, GreaterEqual<Required<PX.Objects.AR.ARInvoice.dueDate>>, And<PX.Objects.CA.Light.ARRegister.dueDate, LessEqual<Required<PX.Objects.AR.ARInvoice.dueDate>>, Or<PX.Objects.CA.Light.ARRegister.dueDate, IsNull>>>>>>, Or<Where<PX.Objects.CA.Light.ARInvoice.discDate, GreaterEqual<Required<PX.Objects.AR.ARInvoice.discDate>>, And<Sub<PX.Objects.CA.Light.ARInvoice.curyDocBal, PX.Objects.AR.ARInvoice.curyDiscBal>, Equal<Required<PX.Objects.AR.ARInvoice.curyDocBal>>, And<PX.Objects.CA.Light.ARInvoice.discDate, LessEqual<Required<PX.Objects.AR.ARInvoice.discDate>>>>>>>>();
      bqlParams.AddRange((IEnumerable<object>) new object[7]
      {
        (object) aDetail.TranDate,
        (object) tranAmount,
        (object) dateRangeForMatch.first,
        (object) dateRangeForMatch.second,
        (object) aDetail.TranDate,
        (object) tranAmount,
        (object) dateRangeForMatch.third
      });
    }
    else
    {
      arInvoiceQuery.WhereAnd<Where2<Where2<Where<PX.Objects.CA.Light.ARInvoice.discDate, Less<Required<PX.Objects.CA.Light.ARInvoice.discDate>>, Or<PX.Objects.CA.Light.ARInvoice.discDate, IsNull>>, And<PX.Objects.CA.Light.ARInvoice.curyDocBal, Equal<Required<PX.Objects.AR.ARInvoice.curyDocBal>>>>, Or<Where<PX.Objects.CA.Light.ARInvoice.discDate, GreaterEqual<Required<PX.Objects.CA.Light.ARInvoice.discDate>>, And<Sub<PX.Objects.CA.Light.ARInvoice.curyDocBal, PX.Objects.CA.Light.ARInvoice.curyDiscBal>, Equal<Required<PX.Objects.AR.ARInvoice.curyDocBal>>>>>>>();
      bqlParams.AddRange((IEnumerable<object>) new object[4]
      {
        (object) aDetail.TranDate,
        (object) tranAmount,
        (object) aDetail.TranDate,
        (object) tranAmount
      });
    }
    if (aDetail.PayeeBAccountID.HasValue)
    {
      arInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.ARInvoice.customerID, In2<Search<PX.Objects.AR.Override.BAccount.bAccountID, Where<PX.Objects.AR.Override.BAccount.bAccountID, Equal<Required<PX.Objects.AR.ARInvoice.customerID>>, Or<Where<PX.Objects.AR.Override.BAccount.parentBAccountID, Equal<Required<PX.Objects.AR.ARInvoice.customerID>>, And<PX.Objects.AR.Override.BAccount.consolidateToParent, Equal<True>>>>>>>>>();
      bqlParams.Add((object) aDetail.PayeeBAccountID);
      bqlParams.Add((object) aDetail.PayeeBAccountID);
    }
    PX.Objects.CA.CashAccount cashAccount1 = (PX.Objects.CA.CashAccount) null;
    if (aSettings.InvoiceFilterByCashAccount.GetValueOrDefault())
    {
      cashAccount1 = PX.Objects.CA.CashAccount.PK.Find(graph, aDetail.CashAccountID);
      arInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.ARInvoice.cashAccountID, Equal<Required<PX.Objects.CA.Light.ARInvoice.cashAccountID>>, Or<Where<PX.Objects.CA.Light.ARInvoice.cashAccountID, IsNull, And<PX.Objects.CA.Light.ARRegister.branchID, Equal<Required<PX.Objects.CA.Light.ARRegister.branchID>>>>>>>();
      bqlParams.Add((object) aDetail.CashAccountID);
      bqlParams.Add((object) cashAccount1.BranchID);
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.interBranch>())
    {
      int[] childBranchIds = PXAccess.GetChildBranchIDs(PXAccess.GetParentOrganizationID((cashAccount1 ?? PX.Objects.CA.CashAccount.PK.Find(graph, aDetail.CashAccountID)).BranchID), true);
      arInvoiceQuery.WhereAnd<Where<PX.Objects.CA.Light.ARRegister.branchID, In<Required<PX.Objects.CA.Light.ARRegister.branchID>>>>();
      bqlParams.Add((object) childBranchIds);
    }
    return arInvoiceQuery;
  }

  public virtual object FindInvoiceByInvoiceInfo<T>(T graph, CABankTran aRow, out string Module) where T : PXGraph, ICABankTransactionsDataProvider
  {
    object invoiceByInvoiceInfo1 = (object) null;
    Module = string.Empty;
    bool? nullable;
    if (aRow.DrCr == "C")
    {
      PXResult<PX.Objects.AP.APInvoice, PX.Objects.AP.APAdjust, PX.Objects.AP.APPayment> invoiceByInvoiceInfo2 = graph.FindAPInvoiceByInvoiceInfo(aRow);
      if (invoiceByInvoiceInfo2 != null)
      {
        PX.Objects.AP.APInvoice apInvoice = PXResult<PX.Objects.AP.APInvoice, PX.Objects.AP.APAdjust, PX.Objects.AP.APPayment>.op_Implicit(invoiceByInvoiceInfo2);
        PX.Objects.AP.APAdjust apAdjust = PXResult<PX.Objects.AP.APInvoice, PX.Objects.AP.APAdjust, PX.Objects.AP.APPayment>.op_Implicit(invoiceByInvoiceInfo2);
        PX.Objects.AP.APPayment apPayment = PXResult<PX.Objects.AP.APInvoice, PX.Objects.AP.APAdjust, PX.Objects.AP.APPayment>.op_Implicit(invoiceByInvoiceInfo2);
        APSetup current = (APSetup) graph.Caches[typeof (APSetup)].Current;
        nullable = apInvoice.Released;
        bool flag1 = false;
        if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
        {
          nullable = apInvoice.Prebooked;
          bool flag2 = false;
          if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
            throw new PXSetPropertyException("Invoice with number {0} is not released. Application can be made only to the released invoices", new object[1]
            {
              (object) aRow.InvoiceInfo
            });
        }
        nullable = apInvoice.OpenDoc;
        bool flag3 = false;
        if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
          throw new PXSetPropertyException("Invoice with number {0} is closed.", new object[1]
          {
            (object) aRow.InvoiceInfo
          });
        nullable = current.EarlyChecks;
        bool flag4 = false;
        if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
        {
          DateTime? docDate = apInvoice.DocDate;
          DateTime? tranDate = aRow.TranDate;
          if ((docDate.HasValue & tranDate.HasValue ? (docDate.GetValueOrDefault() > tranDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            throw new PXSetPropertyException("Invoice with the number {0} is found, but it's date is greater then date of the transaction. It can not  be used for the payment application", new object[1]
            {
              (object) aRow.InvoiceInfo
            });
        }
        if (apAdjust != null && !string.IsNullOrEmpty(apAdjust.AdjgRefNbr))
          throw new PXSetPropertyException("There are unreleased applications to the Invoice number {0}. It can not be used for this payment application.", new object[1]
          {
            (object) aRow.InvoiceInfo
          });
        if (aRow.DrCr == "C" && apPayment != null && !string.IsNullOrEmpty(apPayment.RefNbr))
          throw new PXSetPropertyException("Invoice with the number {0} is found, but there it's used in prepayment or debit adjustment. It can not be used for this payment application.", new object[1]
          {
            (object) aRow.InvoiceInfo
          });
        invoiceByInvoiceInfo1 = (object) apInvoice;
        Module = "AP";
      }
    }
    if (aRow.DrCr == "D")
    {
      PXResult<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARAdjust> invoiceByInvoiceInfo3 = graph.FindARInvoiceByInvoiceInfo(aRow);
      if (invoiceByInvoiceInfo3 != null)
      {
        PX.Objects.AR.ARInvoice arInvoice = PXResult<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARAdjust>.op_Implicit(invoiceByInvoiceInfo3);
        PX.Objects.AR.ARAdjust arAdjust = PXResult<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARAdjust>.op_Implicit(invoiceByInvoiceInfo3);
        nullable = arInvoice.Released;
        bool flag5 = false;
        if (nullable.GetValueOrDefault() == flag5 & nullable.HasValue)
          throw new PXSetPropertyException("Invoice with number {0} is not released. Application can be made only to the released invoices", new object[1]
          {
            (object) aRow.InvoiceInfo
          });
        nullable = arInvoice.OpenDoc;
        bool flag6 = false;
        if (nullable.GetValueOrDefault() == flag6 & nullable.HasValue)
          throw new PXSetPropertyException("Invoice with number {0} is closed.", new object[1]
          {
            (object) aRow.InvoiceInfo
          });
        DateTime? docDate = arInvoice.DocDate;
        DateTime? tranDate = aRow.TranDate;
        if ((docDate.HasValue & tranDate.HasValue ? (docDate.GetValueOrDefault() > tranDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          throw new PXSetPropertyException("Invoice with the number {0} is found, but it's date is greater then date of the transaction. It can not  be used for the payment application", new object[1]
          {
            (object) aRow.InvoiceInfo
          });
        if (arAdjust != null && !string.IsNullOrEmpty(arAdjust.AdjgRefNbr))
          throw new PXSetPropertyException("There are unreleased applications to the Invoice number {0}. It can not be used for this payment application.", new object[1]
          {
            (object) aRow.InvoiceInfo
          });
        invoiceByInvoiceInfo1 = (object) arInvoice;
        Module = "AR";
      }
    }
    return invoiceByInvoiceInfo1;
  }

  public Decimal GetRefNbrCompareWeight(IMatchSettings aSettings)
  {
    Decimal valueOrDefault1 = aSettings.RefNbrCompareWeight.GetValueOrDefault();
    Decimal valueOrDefault2 = aSettings.DateCompareWeight.GetValueOrDefault();
    Decimal valueOrDefault3 = aSettings.PayeeCompareWeight.GetValueOrDefault();
    Decimal num = valueOrDefault1 + valueOrDefault2 + valueOrDefault3;
    return num == 0M ? 0M : valueOrDefault1 / num;
  }
}
