// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PaymentTransaction.ARCashSaleAfterProcessingManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.AR.Standalone;
using PX.Objects.CA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.Extensions.PaymentTransaction;

public class ARCashSaleAfterProcessingManager : AfterProcessingManager
{
  private ARCashSaleEntry graphWithOriginDoc;
  private IBqlTable inputTable;

  public bool ReleaseDoc { get; set; }

  public ARCashSaleEntry Graph { get; set; }

  public override void RunAuthorizeActions(IBqlTable table, bool success)
  {
    this.inputTable = table;
    ARCashSaleEntry graphIfNeeded = this.CreateGraphIfNeeded(table);
    this.ChangeDocProcessingStatus(graphIfNeeded, CCTranType.AuthorizeOnly, success);
    if (success)
      this.UpdateCCBatch(graphIfNeeded);
    this.RestoreCopy();
  }

  public override void RunCaptureActions(IBqlTable table, bool success)
  {
    this.RunCaptureActions(table, CCTranType.AuthorizeAndCapture, success);
  }

  public override void RunPriorAuthorizedCaptureActions(IBqlTable table, bool success)
  {
    this.RunCaptureActions(table, CCTranType.PriorAuthorizedCapture, success);
  }

  public override void RunVoidActions(IBqlTable table, bool success)
  {
    this.inputTable = table;
    CCTranType ccTranType = CCTranType.VoidOrCredit;
    ARCashSaleEntry graphIfNeeded = this.CreateGraphIfNeeded(table);
    this.ChangeDocProcessingStatus(graphIfNeeded, ccTranType, success);
    this.UpdateCashSale(graphIfNeeded, ccTranType, success);
    ARCashSale current = this.Graph.Document.Current;
    if (this.ReleaseDoc = current.VoidAppl.GetValueOrDefault() && this.NeedRelease(current))
      this.ReleaseDocument(graphIfNeeded, ccTranType, success);
    if (success)
      this.UpdateCCBatch(graphIfNeeded);
    this.RestoreCopy();
  }

  public override void RunCreditActions(IBqlTable table, bool success)
  {
    this.inputTable = table;
    CCTranType ccTranType = CCTranType.VoidOrCredit;
    ARCashSaleEntry graphIfNeeded = this.CreateGraphIfNeeded(table);
    this.ChangeDocProcessingStatus(graphIfNeeded, ccTranType, success);
    this.UpdateCashSale(graphIfNeeded, ccTranType, success);
    if (this.NeedRelease(this.Graph.Document.Current))
      this.ReleaseDocument(graphIfNeeded, ccTranType, success);
    if (success)
      this.UpdateCCBatch(graphIfNeeded);
    this.RestoreCopy();
  }

  public override bool CheckDocStateConsistency(IBqlTable table)
  {
    bool flag1 = true;
    ARCashSaleEntry graphIfNeeded = this.CreateGraphIfNeeded(table);
    ARCashSale doc = graphIfNeeded.Document.Current;
    if (doc == null)
      return flag1;
    PX.Objects.AR.ExternalTransaction extTran = !doc.CCActualExternalTransactionID.HasValue ? this.GetLastProcessedExternalTran(graphIfNeeded) : graphIfNeeded.ExternalTran.Select().RowCast<PX.Objects.AR.ExternalTransaction>().Where<PX.Objects.AR.ExternalTransaction>((Func<PX.Objects.AR.ExternalTransaction, bool>) (i =>
    {
      int? transactionId = i.TransactionID;
      int? externalTransactionId = doc.CCActualExternalTransactionID;
      return transactionId.GetValueOrDefault() == externalTransactionId.GetValueOrDefault() & transactionId.HasValue == externalTransactionId.HasValue;
    })).FirstOrDefault<PX.Objects.AR.ExternalTransaction>();
    if (extTran != null)
    {
      bool? needSync = extTran.NeedSync;
      bool flag2 = false;
      if (needSync.GetValueOrDefault() == flag2 & needSync.HasValue)
      {
        ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) graphIfNeeded, (IExternalTransaction) extTran);
        if (transactionState.IsPreAuthorized && !transactionState.IsOpenForReview)
        {
          bool? isCcAuthorized = doc.IsCCAuthorized;
          bool flag3 = false;
          if (isCcAuthorized.GetValueOrDefault() == flag3 & isCcAuthorized.HasValue)
            goto label_13;
        }
        if ((transactionState.IsPreAuthorized || transactionState.IsCaptured) && transactionState.IsOpenForReview)
        {
          bool? isCcUserAttention = doc.IsCCUserAttention;
          bool flag4 = false;
          if (isCcUserAttention.GetValueOrDefault() == flag4 & isCcUserAttention.HasValue)
            goto label_13;
        }
        bool? nullable;
        if ((transactionState.IsCaptured || transactionState.IsRefunded) && !transactionState.IsOpenForReview)
        {
          nullable = doc.PendingProcessing;
          if (nullable.GetValueOrDefault())
            goto label_13;
        }
        if (transactionState.IsVoided)
        {
          nullable = doc.IsCCCaptured;
          if (!nullable.GetValueOrDefault())
          {
            nullable = doc.IsCCAuthorized;
            if (!nullable.GetValueOrDefault())
              goto label_14;
          }
        }
        else
          goto label_14;
label_13:
        flag1 = false;
      }
    }
label_14:
    return flag1;
  }

  private void RunCaptureActions(IBqlTable table, CCTranType tranType, bool success)
  {
    this.inputTable = table;
    ARCashSaleEntry graphIfNeeded = this.CreateGraphIfNeeded(table);
    this.ChangeDocProcessingStatus(graphIfNeeded, tranType, success);
    this.UpdateCashSale(graphIfNeeded, tranType, success);
    if (success)
      this.UpdateDocCleared(graphIfNeeded);
    if (this.NeedRelease(this.Graph.Document.Current))
      this.ReleaseDocument(graphIfNeeded, tranType, success);
    if (success)
      this.UpdateCCBatch(graphIfNeeded);
    this.RestoreCopy();
  }

  private void UpdateCCBatch(ARCashSaleEntry arGraph)
  {
    ARCashSale current = arGraph.Document.Current;
    PX.Objects.AR.ExternalTransaction processedExternalTran = this.GetLastProcessedExternalTran(arGraph);
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) arGraph, (IExternalTransaction) processedExternalTran);
    if (transactionState.NeedSync || transactionState.SyncFailed)
      return;
    foreach (PXResult<CCBatch, CCBatchTransaction> pxResult1 in PXSelectBase<CCBatch, PXSelectJoin<CCBatch, InnerJoin<CCBatchTransaction, On<CCBatch.batchID, Equal<CCBatchTransaction.batchID>>>, Where<CCBatch.processingCenterID, Equal<Required<CCBatch.processingCenterID>>, And<CCBatchTransaction.pCTranNumber, Equal<Required<CCBatchTransaction.pCTranNumber>>, And<CCBatchTransaction.processingStatus, In3<CCBatchTranProcessingStatusCode.missing, CCBatchTranProcessingStatusCode.pendingProcessing>>>>>.Config>.SelectSingleBound((PXGraph) arGraph, (object[]) null, (object) processedExternalTran.ProcessingCenterID, (object) processedExternalTran.TranNumber))
    {
      CCBatch ccBatch = (CCBatch) pxResult1;
      CCBatchTransaction batchTransaction1 = (CCBatchTransaction) pxResult1;
      if (ccBatch != null && batchTransaction1 != null)
      {
        CCBatchMaint instance = PXGraph.CreateInstance<CCBatchMaint>();
        instance.BatchView.Current = ccBatch;
        instance.Transactions.Select();
        batchTransaction1.TransactionID = processedExternalTran.TransactionID;
        batchTransaction1.OriginalStatus = processedExternalTran.ProcStatus;
        batchTransaction1.DocType = current.DocType;
        batchTransaction1.RefNbr = current.RefNbr;
        batchTransaction1.CurrentStatus = batchTransaction1.OriginalStatus;
        batchTransaction1.ProcessingStatus = "PRD";
        CCBatchTransaction batchTransaction2 = instance.Transactions.Update(batchTransaction1);
        bool flag = true;
        foreach (PXResult<CCBatchTransaction> pxResult2 in instance.Transactions.Select())
        {
          CCBatchTransaction batchTransaction3 = (CCBatchTransaction) pxResult2;
          if (batchTransaction3.ProcessingStatus != "PRD" && batchTransaction3.ProcessingStatus != "HID")
          {
            flag = false;
            break;
          }
        }
        if (flag)
          ccBatch.Status = "PRD";
        instance.BatchView.UpdateCurrent();
        instance.Save.Press();
        if (CCBatchMaint.MatchStatuses(batchTransaction2.SettlementStatus, processedExternalTran, (PX.Objects.AR.ARRegister) current, (PXGraph) arGraph) == CCBatchMaint.StatusMatchingResult.SuccessMatch)
        {
          processedExternalTran.Settled = new bool?(true);
          arGraph.ExternalTran.Update(processedExternalTran);
          current.Settled = new bool?(true);
          current.Cleared = new bool?(true);
          current.ClearDate = ccBatch.SettlementTime;
          arGraph.Caches[typeof (ARCashSale)].Update((object) current);
          arGraph.Save.Press();
        }
        arGraph.Save.Press();
      }
    }
  }

  private bool NeedRelease(ARCashSale cashSale)
  {
    if (this.ReleaseDoc)
    {
      bool? released = cashSale.Released;
      bool flag = false;
      if (released.GetValueOrDefault() == flag & released.HasValue)
        return CCProcessingHelper.IntegratedProcessingActivated(this.Graph.arsetup.Current);
    }
    return false;
  }

  public void UpdateCashSale(ARCashSaleEntry graph, CCTranType tranType, bool success)
  {
    if (!success)
      return;
    ARCashSaleEntry graph1 = graph;
    ARCashSale current = graph1.Document.Current;
    IExternalTransaction processedExternalTran = (IExternalTransaction) this.GetLastProcessedExternalTran(graph);
    if (processedExternalTran == null)
      return;
    ARCashSale arCashSale1 = current;
    System.DateTime? lastActivityDate = processedExternalTran.LastActivityDate;
    System.DateTime? nullable1 = new System.DateTime?(lastActivityDate.Value.Date);
    arCashSale1.DocDate = nullable1;
    ARCashSale arCashSale2 = current;
    lastActivityDate = processedExternalTran.LastActivityDate;
    System.DateTime? nullable2 = new System.DateTime?(lastActivityDate.Value.Date);
    arCashSale2.AdjDate = nullable2;
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) graph1, processedExternalTran);
    if (transactionState.IsActive)
      current.ExtRefNbr = processedExternalTran.TranNumber;
    else if (current.DocType != "RCS" && (transactionState.IsVoided || transactionState.IsDeclined))
      current.ExtRefNbr = (string) null;
    graph1.Document.Update(current);
  }

  private void UpdateDocCleared(ARCashSaleEntry graph)
  {
    IExternalTransaction processedExternalTran = (IExternalTransaction) this.GetLastProcessedExternalTran(graph);
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) graph, processedExternalTran);
    if ((transactionState != null ? (transactionState.IsCaptured ? 1 : 0) : 0) == 0)
      return;
    ARCashSale current = graph.Document.Current;
    bool? settlementBatches = CCProcessingCenter.PK.Find((PXGraph) graph, current.ProcessingCenterID).ImportSettlementBatches;
    bool flag = false;
    if (!(settlementBatches.GetValueOrDefault() == flag & settlementBatches.HasValue))
      return;
    current.Cleared = new bool?(true);
    current.ClearDate = new System.DateTime?(processedExternalTran.LastActivityDate.Value.Date);
  }

  public void ReleaseDocument(ARCashSaleEntry cashSaleGraph, CCTranType procTran, bool success)
  {
    if (!(cashSaleGraph.Document.Current != null & success))
      return;
    PX.Objects.AR.ExternalTransaction processedExternalTran = this.GetLastProcessedExternalTran(cashSaleGraph);
    if (processedExternalTran == null)
      return;
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) cashSaleGraph, (IExternalTransaction) processedExternalTran);
    if (transactionState.IsDeclined || transactionState.IsOpenForReview)
      return;
    this.PersistData();
    ARCashSale current = cashSaleGraph.Document.Current;
    PaymentTransactionGraph<ARCashSaleEntry, ARCashSale>.ReleaseARDocument((IBqlTable) current);
    cashSaleGraph.Document.Current = PrimaryKeyOf<ARCashSale>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<ARCashSale.docType, ARCashSale.refNbr>>.Find((PXGraph) cashSaleGraph, current);
  }

  private void ChangeDocProcessingStatus(
    ARCashSaleEntry cashSaleGraph,
    CCTranType tranType,
    bool success)
  {
    ARCashSale current = cashSaleGraph.Document.Current;
    PX.Objects.AR.ExternalTransaction processedExternalTran = this.GetLastProcessedExternalTran(cashSaleGraph);
    if (processedExternalTran == null)
      return;
    this.DeactivateExpiredTrans(cashSaleGraph);
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) cashSaleGraph, (IExternalTransaction) processedExternalTran);
    this.ChangeCaptureFailedFlag(transactionState, current);
    this.ChangeUserAttentionFlag(transactionState, cashSaleGraph);
    if (success)
    {
      bool flag = true;
      if (processedExternalTran != null)
      {
        if (transactionState.IsCaptured && !transactionState.IsOpenForReview)
          flag = false;
        if (current.DocType == "RCS" && (transactionState.IsVoided || transactionState.IsRefunded) && !transactionState.IsOpenForReview)
          flag = false;
        if (current.Released.GetValueOrDefault())
          flag = false;
        this.ChangeDocProcessingFlags(transactionState, current, tranType);
      }
      current.PendingProcessing = new bool?(flag);
    }
    this.ChangeOriginDocProcessingStatus(transactionState, cashSaleGraph, tranType, success);
    ARCashSale arCashSale = this.SyncActualExternalTransation(transactionState, cashSaleGraph, current);
    cashSaleGraph.Document.Update(arCashSale);
  }

  private void DeactivateExpiredTrans(ARCashSaleEntry graph)
  {
    PXCache cache = graph.Document.Cache;
    foreach (PX.Objects.AR.ExternalTransaction externalTransaction in graph.ExternalTran.Select().RowCast<PX.Objects.AR.ExternalTransaction>().Where<PX.Objects.AR.ExternalTransaction>((Func<PX.Objects.AR.ExternalTransaction, bool>) (i => ExternalTranHelper.IsExpired((IExternalTransaction) i) && i.Active.GetValueOrDefault())))
    {
      externalTransaction.Active = new bool?(false);
      externalTransaction.ProcStatus = "AUE";
      graph.ExternalTran.Update(externalTransaction);
    }
  }

  private void ChangeUserAttentionFlag(ExternalTransactionState state, ARCashSaleEntry graph)
  {
    ARCashSale current = graph.Document.Current;
    bool flag = false;
    if (state.IsOpenForReview || current.IsCCCaptureFailed.GetValueOrDefault() || EnumerableExtensions.IsIn<ProcessingStatus>(state.ProcessingStatus, ProcessingStatus.VoidFail, ProcessingStatus.VoidDecline, ProcessingStatus.CreditFail, ProcessingStatus.CreditDecline))
      flag = true;
    current.IsCCUserAttention = new bool?(flag);
  }

  private void ChangeOriginDocProcessingStatus(
    ExternalTransactionState tranState,
    ARCashSaleEntry cashSaleGraph,
    CCTranType tranType,
    bool success)
  {
    IExternalTransaction externalTransaction = tranState.ExternalTransaction;
    ARCashSaleEntry graphWithOriginDoc = this.GetGraphWithOriginDoc(tranState, cashSaleGraph, tranType);
    if (graphWithOriginDoc == null)
      return;
    PX.Objects.AR.ExternalTransaction processedExternalTran = this.GetLastProcessedExternalTran(graphWithOriginDoc);
    ARCashSale current = graphWithOriginDoc.Document.Current;
    int? transactionId1 = processedExternalTran.TransactionID;
    int? transactionId2 = externalTransaction.TransactionID;
    if (transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue)
    {
      this.ChangeCaptureFailedFlag(tranState, current);
      if (success)
        this.ChangeDocProcessingFlags(tranState, current, tranType);
    }
    cashSaleGraph.Caches[typeof (ARCashSale)].Update((object) current);
  }

  private void ChangeDocProcessingFlags(
    ExternalTransactionState tranState,
    ARCashSale doc,
    CCTranType tranType)
  {
    if (tranState.HasErrors)
      return;
    ARCashSale arCashSale1 = doc;
    ARCashSale arCashSale2 = doc;
    ARCashSale arCashSale3 = doc;
    bool? nullable1 = new bool?(false);
    bool? nullable2 = nullable1;
    arCashSale3.IsCCRefunded = nullable2;
    bool? nullable3;
    bool? nullable4 = nullable3 = nullable1;
    arCashSale2.IsCCCaptured = nullable3;
    bool? nullable5 = nullable4;
    arCashSale1.IsCCAuthorized = nullable5;
    if (tranState.IsDeclined || tranState.IsOpenForReview || ExternalTranHelper.IsExpired(tranState.ExternalTransaction))
      return;
    switch (tranType)
    {
      case CCTranType.AuthorizeAndCapture:
        doc.IsCCCaptured = new bool?(true);
        break;
      case CCTranType.AuthorizeOnly:
        doc.IsCCAuthorized = new bool?(true);
        break;
      case CCTranType.PriorAuthorizedCapture:
        doc.IsCCCaptured = new bool?(true);
        break;
      case CCTranType.CaptureOnly:
        doc.IsCCCaptured = new bool?(true);
        break;
      case CCTranType.Credit:
        doc.IsCCRefunded = new bool?(true);
        break;
    }
    if (tranType != CCTranType.VoidOrCredit || !tranState.IsRefunded)
      return;
    doc.IsCCRefunded = new bool?(true);
  }

  private void ChangeCaptureFailedFlag(ExternalTransactionState state, ARCashSale doc)
  {
    bool? isCcCaptureFailed = doc.IsCCCaptureFailed;
    bool flag = false;
    if (isCcCaptureFailed.GetValueOrDefault() == flag & isCcCaptureFailed.HasValue && (state.ProcessingStatus == ProcessingStatus.CaptureFail || state.ProcessingStatus == ProcessingStatus.CaptureDecline))
    {
      doc.IsCCCaptureFailed = new bool?(true);
    }
    else
    {
      if (!doc.IsCCCaptureFailed.GetValueOrDefault() || !state.IsCaptured && !state.IsVoided && (!state.IsPreAuthorized || state.HasErrors || this.CheckCaptureFailedExists(state)))
        return;
      doc.IsCCCaptureFailed = new bool?(false);
    }
  }

  private bool CheckCaptureFailedExists(ExternalTransactionState state)
  {
    bool flag = false;
    if (CCProcTranHelper.HasCaptureFailed((IEnumerable<ICCPaymentTransaction>) this.GetPaymentProcessingRepository().GetCCProcTranByTranID(state.ExternalTransaction.TransactionID)))
      flag = true;
    return flag;
  }

  private ARCashSaleEntry GetGraphByDocTypeRefNbr(string docType, string refNbr)
  {
    ARCashSaleEntry instance = PXGraph.CreateInstance<ARCashSaleEntry>();
    instance.Document.Current = (ARCashSale) PXSelectBase<ARCashSale, PXSelect<ARCashSale, Where<ARCashSale.docType, Equal<Required<ARCashSale.docType>>, And<ARCashSale.refNbr, Equal<Required<ARCashSale.refNbr>>>>>.Config>.SelectWindowed((PXGraph) instance, 0, 1, (object) docType, (object) refNbr);
    return instance;
  }

  public override void PersistData()
  {
    ARCashSale current = this.Graph?.Document.Current;
    if (current != null && this.Graph.Document.Cache.GetStatus((object) current) != PXEntryStatus.Notchanged)
      this.Graph.Save.Press();
    this.RestoreCopy();
  }

  private ARCashSaleEntry GetGraphWithOriginDoc(
    ExternalTransactionState state,
    ARCashSaleEntry graph,
    CCTranType tranType)
  {
    if (this.graphWithOriginDoc != null)
      return this.graphWithOriginDoc;
    IExternalTransaction externalTransaction = state.ExternalTransaction;
    ARCashSale current = graph.Document.Current;
    if (tranType == CCTranType.VoidOrCredit && externalTransaction.DocType == current.OrigDocType && externalTransaction.RefNbr == current.OrigRefNbr && externalTransaction.DocType == "CSL")
      this.graphWithOriginDoc = this.GetGraphByDocTypeRefNbr(externalTransaction.DocType, externalTransaction.RefNbr);
    return this.graphWithOriginDoc;
  }

  protected virtual ARCashSaleEntry CreateGraphIfNeeded(IBqlTable table)
  {
    if (this.Graph == null)
    {
      ARCashSale arCashSale = table as ARCashSale;
      this.Graph = this.GetGraphByDocTypeRefNbr(arCashSale.DocType, arCashSale.RefNbr);
      this.Graph.Document.Update(arCashSale);
    }
    return this.Graph;
  }

  protected virtual ICCPaymentProcessingRepository GetPaymentProcessingRepository()
  {
    return (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository((PXGraph) this.Graph);
  }

  public override PXGraph GetGraph() => (PXGraph) this.Graph;

  private void RestoreCopy()
  {
    ARCashSale current = this.Graph?.Document.Current;
    if (current == null || this.inputTable == null)
      return;
    this.Graph.Document.Cache.RestoreCopy((object) this.inputTable, (object) current);
  }

  private PX.Objects.AR.ExternalTransaction GetLastProcessedExternalTran(
    ARCashSaleEntry cashSaleGraph)
  {
    IEnumerable<PX.Objects.AR.ExternalTransaction> externalTransactions = cashSaleGraph.ExternalTran.Select().RowCast<PX.Objects.AR.ExternalTransaction>();
    if (externalTransactions.Count<PX.Objects.AR.ExternalTransaction>() == 0)
      return (PX.Objects.AR.ExternalTransaction) null;
    IEnumerable<CCProcTran> trans = cashSaleGraph.ccProcTran.Select().RowCast<CCProcTran>().Where<CCProcTran>((Func<CCProcTran, bool>) (i => i.ProcStatus != "OPN"));
    IExternalTransaction extTran = ExternalTranHelper.GetLastProcessedExtTran((IEnumerable<IExternalTransaction>) externalTransactions, (IEnumerable<ICCPaymentTransaction>) trans);
    return extTran == null ? (PX.Objects.AR.ExternalTransaction) null : externalTransactions.Where<PX.Objects.AR.ExternalTransaction>((Func<PX.Objects.AR.ExternalTransaction, bool>) (i =>
    {
      int? transactionId1 = i.TransactionID;
      int? transactionId2 = extTran.TransactionID;
      return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
    })).FirstOrDefault<PX.Objects.AR.ExternalTransaction>();
  }

  private ARCashSale SyncActualExternalTransation(
    ExternalTransactionState state,
    ARCashSaleEntry cashSaleGraph,
    ARCashSale cashSale)
  {
    IExternalTransaction externalTransaction = state.ExternalTransaction;
    if (!cashSale.CCActualExternalTransactionID.HasValue)
    {
      cashSale.CCActualExternalTransactionID = externalTransaction.TransactionID;
      return cashSale;
    }
    int? transactionId = externalTransaction.TransactionID;
    int? externalTransactionId = cashSale.CCActualExternalTransactionID;
    if (transactionId.GetValueOrDefault() > externalTransactionId.GetValueOrDefault() & transactionId.HasValue & externalTransactionId.HasValue)
    {
      PX.Objects.AR.ExternalTransaction extTran = (PX.Objects.AR.ExternalTransaction) cashSaleGraph.ExternalTran.Select().SingleOrDefault<PXResult<PX.Objects.AR.ExternalTransaction>>((Expression<Func<PXResult<PX.Objects.AR.ExternalTransaction>, bool>>) (t => ((PX.Objects.AR.ExternalTransaction) t).TransactionID == cashSale.CCActualExternalTransactionID));
      if (externalTransaction == null || extTran == null)
        return cashSale;
      ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) cashSaleGraph, externalTransaction);
      if (!ExternalTranHelper.GetTransactionState((PXGraph) cashSaleGraph, (IExternalTransaction) extTran).IsActive || transactionState.IsActive)
        cashSale.CCActualExternalTransactionID = externalTransaction.TransactionID;
    }
    return cashSale;
  }
}
