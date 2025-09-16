// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PaymentTransaction.ARPaymentAfterProcessingManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.CA;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.ARPaymentEntryExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.Extensions.PaymentTransaction;

public class ARPaymentAfterProcessingManager : AfterProcessingManager
{
  private ARPaymentEntry graphWithOriginDoc;
  private IBqlTable inputTable;
  private bool importDeactivatedTran;

  public bool ReleaseDoc { get; set; }

  public bool RaisedVoidForReAuthorization { get; set; }

  public bool NeedSyncContext { get; set; }

  public ARPaymentEntry Graph { get; set; }

  public override void RunAuthorizeActions(IBqlTable table, bool success)
  {
    this.RunAuthorizeActions(table, CCTranType.AuthorizeOnly, success);
  }

  public override void RunIncreaseAuthorizedAmountActions(IBqlTable table, bool success)
  {
    this.RunAuthorizeActions(table, CCTranType.IncreaseAuthorizedAmount, success);
  }

  private void RunAuthorizeActions(IBqlTable table, CCTranType tranType, bool success)
  {
    this.inputTable = table;
    ARPaymentEntry graphIfNeeded = this.CreateGraphIfNeeded(table);
    this.ChangeDocProcessingStatus(graphIfNeeded, tranType, success);
    if (this.RaisedVoidForReAuthorization)
      this.UpdateDocReAuthFieldsAfterValidationByVoidForReAuth(graphIfNeeded);
    else
      this.UpdateDocReAuthFieldsAfterAuthorize(graphIfNeeded);
    this.UpdateARPayment(graphIfNeeded, tranType, success);
    this.UpdatePaymentAmountIfNeeded(graphIfNeeded);
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
    CCTranType tranType = CCTranType.VoidOrCredit;
    ARPaymentEntry graphIfNeeded = this.CreateGraphIfNeeded(table);
    if (this.CheckImportDeactivatedTran(graphIfNeeded))
      return;
    PX.Objects.AR.ARPayment current = graphIfNeeded.Document.Current;
    if (!this.CreateVoidDocument(graphIfNeeded, success))
    {
      this.ChangeDocProcessingStatus(graphIfNeeded, tranType, success);
      if (this.RaisedVoidForReAuthorization)
        this.UpdateDocReAuthFieldsAfterVoidForReAuth(graphIfNeeded);
      else
        this.UpdateDocReAuthFieldsAfterVoid(graphIfNeeded);
      this.UpdateARPayment(graphIfNeeded, tranType, success);
      if (this.ReleaseDoc && this.NeedRelease(current) && (ARPaymentType.VoidAppl(current.DocType) || current.DocType == "REF"))
        this.ReleaseDocument(graphIfNeeded, tranType, success);
      else
        this.VoidOriginalPayment(graphIfNeeded, success);
    }
    if (success)
      this.UpdateCCBatch(graphIfNeeded);
    this.RestoreCopy();
  }

  public override void RunCreditActions(IBqlTable table, bool success)
  {
    this.inputTable = table;
    CCTranType tranType = CCTranType.VoidOrCredit;
    ARPaymentEntry graphIfNeeded = this.CreateGraphIfNeeded(table);
    if (this.CheckImportDeactivatedTran(graphIfNeeded))
      return;
    int num = this.CreateVoidDocument(graphIfNeeded, success) ? 1 : 0;
    PX.Objects.AR.ARPayment current = this.Graph.Document.Current;
    if (num == 0)
    {
      this.ChangeDocProcessingStatus(graphIfNeeded, tranType, success);
      this.UpdateARPaymentAndSetWarning(graphIfNeeded, tranType, success);
      if (this.ReleaseDoc && this.NeedRelease(current))
        this.ReleaseDocument(graphIfNeeded, tranType, success);
    }
    if (success)
      this.UpdateCCBatch(graphIfNeeded);
    this.RestoreCopy();
  }

  public override void RunCaptureOnlyActions(IBqlTable table, bool success)
  {
    this.inputTable = table;
    CCTranType tranType = CCTranType.CaptureOnly;
    ARPaymentEntry graphIfNeeded = this.CreateGraphIfNeeded(table);
    this.ChangeDocProcessingStatus(graphIfNeeded, tranType, success);
    this.UpdateDocReAuthFieldsAfterCapture(graphIfNeeded);
    this.UpdateExtRefNbrARPayment(graphIfNeeded, tranType, success);
    PX.Objects.AR.ARPayment current = this.Graph.Document.Current;
    if (this.ReleaseDoc && this.NeedRelease(current) && current.DocType != "REF")
      this.ReleaseDocument(graphIfNeeded, tranType, success);
    if (success)
      this.UpdateCCBatch(graphIfNeeded);
    this.RestoreCopy();
  }

  public override void RunUnknownActions(IBqlTable table, bool success)
  {
    this.inputTable = table;
    ARPaymentEntry graphIfNeeded = this.CreateGraphIfNeeded(table);
    if (this.CheckImportDeactivatedTran(graphIfNeeded))
      return;
    this.SyncActualExternalTransation(graphIfNeeded, graphIfNeeded.Document.Current);
    this.ChangeUserAttentionFlag(graphIfNeeded);
    this.UpdateARPayment(graphIfNeeded, CCTranType.Unknown, success);
    PX.Objects.AR.ARPayment current = graphIfNeeded.Document.Current;
    graphIfNeeded.Document.Update(current);
    this.RestoreCopy();
  }

  public override bool CheckDocStateConsistency(IBqlTable table)
  {
    bool flag1 = true;
    ARPaymentEntry graphIfNeeded = this.CreateGraphIfNeeded(table);
    PX.Objects.AR.ARPayment current = graphIfNeeded.Document.Current;
    if (current == null)
      return flag1;
    PX.Objects.AR.ExternalTransaction processedExternalTran = this.GetLastProcessedExternalTran(graphIfNeeded);
    if (processedExternalTran != null)
    {
      bool? nullable = processedExternalTran.NeedSync;
      bool flag2 = false;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      {
        ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) graphIfNeeded, (IExternalTransaction) processedExternalTran);
        if (transactionState.IsPreAuthorized && !transactionState.IsOpenForReview)
        {
          nullable = current.IsCCAuthorized;
          bool flag3 = false;
          if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
            goto label_17;
        }
        if ((transactionState.IsPreAuthorized || transactionState.IsCaptured) && transactionState.IsOpenForReview)
        {
          nullable = current.IsCCUserAttention;
          bool flag4 = false;
          if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
            goto label_17;
        }
        if (transactionState.IsCaptured && this.PaymentDocType(current) && !transactionState.IsOpenForReview)
        {
          nullable = current.PendingProcessing;
          if (nullable.GetValueOrDefault())
            goto label_17;
        }
        if (transactionState.IsRefunded && EnumerableExtensions.IsIn<string>(current.DocType, "RPM", "REF") && !transactionState.IsOpenForReview)
        {
          nullable = current.PendingProcessing;
          if (nullable.GetValueOrDefault())
            goto label_17;
        }
        if (transactionState.IsVoided)
        {
          nullable = current.IsCCCaptured;
          if (!nullable.GetValueOrDefault())
          {
            nullable = current.IsCCAuthorized;
            if (nullable.GetValueOrDefault())
              goto label_17;
          }
          else
            goto label_17;
        }
        if (transactionState.IsPreAuthorized)
        {
          Decimal? amount = processedExternalTran.Amount;
          Decimal? curyDocBal = current.CuryDocBal;
          if (amount.GetValueOrDefault() == curyDocBal.GetValueOrDefault() & amount.HasValue == curyDocBal.HasValue)
            goto label_18;
        }
        else
          goto label_18;
label_17:
        flag1 = false;
      }
    }
label_18:
    return flag1;
  }

  public bool NeedReleaseForCapture(PX.Objects.AR.ARPayment doc)
  {
    bool flag = false;
    try
    {
      ARPaymentEntry.CheckValidPeriodForCCTran((PXGraph) this.Graph, doc);
      flag = this.NeedRelease(doc) && doc.DocType != "REF";
    }
    catch
    {
    }
    return flag;
  }

  private void RunCaptureActions(IBqlTable table, CCTranType tranType, bool success)
  {
    this.inputTable = table;
    ARPaymentEntry graphIfNeeded = this.CreateGraphIfNeeded(table);
    this.ChangeDocProcessingStatus(graphIfNeeded, tranType, success);
    this.UpdateDocReAuthFieldsAfterCapture(graphIfNeeded);
    this.UpdateARPaymentAndSetWarning(graphIfNeeded, tranType, success);
    this.UpdatePaymentAmountIfNeeded(graphIfNeeded);
    if (success)
      this.UpdateDocCleared(graphIfNeeded);
    PX.Objects.AR.ARPayment current = graphIfNeeded.Document.Current;
    if (this.ReleaseDoc && this.NeedReleaseForCapture(current))
      this.ReleaseDocument(graphIfNeeded, tranType, success);
    if (this.IsMassProcess)
      this.CheckForHeldForReviewStatusAfterProc(graphIfNeeded, tranType, success);
    if (success)
      this.UpdateCCBatch(graphIfNeeded);
    this.RestoreCopy();
  }

  private void UpdateCCBatch(ARPaymentEntry arGraph)
  {
    PX.Objects.AR.ARPayment current = arGraph.Document.Current;
    PX.Objects.AR.ExternalTransaction processedExternalTran = this.GetLastProcessedExternalTran(arGraph);
    if (processedExternalTran == null)
      return;
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) arGraph, (IExternalTransaction) processedExternalTran);
    if (transactionState.NeedSync || transactionState.SyncFailed)
      return;
    foreach (PXResult<CCBatch, CCBatchTransaction> pxResult1 in PXSelectBase<CCBatch, PXSelectJoin<CCBatch, InnerJoin<CCBatchTransaction, On<CCBatch.batchID, Equal<CCBatchTransaction.batchID>>>, Where<CCBatch.processingCenterID, Equal<Required<CCBatch.processingCenterID>>, And<CCBatchTransaction.pCTranNumber, Equal<Required<CCBatchTransaction.pCTranNumber>>, And<CCBatchTransaction.processingStatus, In3<CCBatchTranProcessingStatusCode.missing, CCBatchTranProcessingStatusCode.pendingProcessing>>>>>.Config>.SelectSingleBound((PXGraph) arGraph, (object[]) null, (object) processedExternalTran.ProcessingCenterID, (object) processedExternalTran.TranNumber))
    {
      CCBatch ccBatch = (CCBatch) pxResult1;
      CCBatchTransaction batchTransaction1 = (CCBatchTransaction) pxResult1;
      if (ccBatch != null && batchTransaction1 != null)
      {
        batchTransaction1.TransactionID = processedExternalTran.TransactionID;
        batchTransaction1.OriginalStatus = processedExternalTran.ProcStatus;
        batchTransaction1.DocType = current.DocType;
        batchTransaction1.RefNbr = current.RefNbr;
        batchTransaction1.CurrentStatus = batchTransaction1.OriginalStatus;
        batchTransaction1.ProcessingStatus = "PRD";
        CCBatchTransaction batchTransaction2 = arGraph.BatchTran.Update(batchTransaction1);
        CCBatchMaint instance = PXGraph.CreateInstance<CCBatchMaint>();
        instance.BatchView.Current = ccBatch;
        bool flag = true;
        foreach (PXResult<CCBatchTransaction> pxResult2 in instance.Transactions.Select())
        {
          CCBatchTransaction batchTransaction3 = (CCBatchTransaction) pxResult2;
          if (batchTransaction3.PCTranNumber != batchTransaction2.PCTranNumber && EnumerableExtensions.IsNotIn<string>(batchTransaction3.ProcessingStatus, "PRD", "HID"))
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
          arGraph.Caches[typeof (PX.Objects.AR.ARPayment)].Update((object) current);
        }
        arGraph.Save.Press();
      }
    }
  }

  private void UpdatePaymentAmountIfNeeded(ARPaymentEntry graph)
  {
    PX.Objects.AR.ARPayment current = graph.Document.Current;
    PX.Objects.AR.ExternalTransaction processedExternalTran = this.GetLastProcessedExternalTran(graph);
    if (processedExternalTran == null)
      return;
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) graph, (IExternalTransaction) processedExternalTran);
    int num1;
    if (transactionState.IsPreAuthorized)
    {
      Decimal? amount = processedExternalTran.Amount;
      Decimal? curyOrigDocAmt = current.CuryOrigDocAmt;
      num1 = amount.GetValueOrDefault() > curyOrigDocAmt.GetValueOrDefault() & amount.HasValue & curyOrigDocAmt.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    if (!transactionState.IsCaptured && !transactionState.IsPreAuthorized)
      return;
    Decimal? amount1 = processedExternalTran.Amount;
    Decimal? curyOrigDocAmt1 = current.CuryOrigDocAmt;
    if (amount1.GetValueOrDefault() == curyOrigDocAmt1.GetValueOrDefault() & amount1.HasValue == curyOrigDocAmt1.HasValue)
      return;
    bool? released = current.Released;
    bool flag2 = false;
    if (!(released.GetValueOrDefault() == flag2 & released.HasValue))
      return;
    this.UpdateAdjustmentIfNeeded(graph);
    int num2 = this.ProcessAdjustments(graph, processedExternalTran) ? 1 : 0;
    current.CuryOrigDocAmt = processedExternalTran.Amount;
    if (num2 == 0 && !flag1)
      current.Hold = new bool?(true);
    graph.Document.Update(current);
  }

  private void UpdateAdjustmentIfNeeded(ARPaymentEntry graph)
  {
    Payment payment = graph.Document.Current.GetExtension<Payment>();
    if (payment == null || !payment.CuryDocBalIncrease.HasValue || payment.TransactionOrigDocType == null || payment.TransactionOrigDocRefNbr == null)
      return;
    OrdersToApplyTab applyTabExtension = graph.GetOrdersToApplyTabExtension();
    IEnumerable<SOAdjust> source1 = applyTabExtension != null ? applyTabExtension.SOAdjustments.Select().RowCast<SOAdjust>() : (IEnumerable<SOAdjust>) null;
    Decimal? curyAdjgAmt;
    Decimal? nullable1;
    Decimal? nullable2;
    if (source1.Any<SOAdjust>())
    {
      SOAdjust soAdjust = source1.Where<SOAdjust>((Func<SOAdjust, bool>) (a => a.AdjdOrderType == payment.TransactionOrigDocType && a.AdjdOrderNbr == payment.TransactionOrigDocRefNbr)).SingleOrDefault<SOAdjust>();
      if (!payment.OrigDocAppliedAmount.HasValue && soAdjust != null)
      {
        Payment payment1 = payment;
        curyAdjgAmt = soAdjust.CuryAdjgAmt;
        Decimal? curyDocBalIncrease = payment.CuryDocBalIncrease;
        nullable1 = curyAdjgAmt.HasValue & curyDocBalIncrease.HasValue ? new Decimal?(curyAdjgAmt.GetValueOrDefault() + curyDocBalIncrease.GetValueOrDefault()) : new Decimal?();
        Decimal? curyDocBal = payment.CuryDocBal;
        Decimal? nullable3 = nullable1.HasValue & curyDocBal.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - curyDocBal.GetValueOrDefault()) : new Decimal?();
        payment1.OrigDocAppliedAmount = nullable3;
      }
      if (soAdjust != null)
      {
        nullable2 = soAdjust.CuryAdjgAmt;
        nullable1 = payment.OrigDocAppliedAmount;
        if (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
        {
          nullable1 = soAdjust.CuryDocBal;
          Decimal? docAppliedAmount = payment.OrigDocAppliedAmount;
          curyAdjgAmt = soAdjust.CuryAdjgAmt;
          nullable2 = docAppliedAmount.HasValue & curyAdjgAmt.HasValue ? new Decimal?(docAppliedAmount.GetValueOrDefault() - curyAdjgAmt.GetValueOrDefault()) : new Decimal?();
          if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
          {
            if (PXLongOperation.IsLongOperationContext())
            {
              PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = graph.GetExtension<ARPaymentEntry.MultiCurrency>().GetCurrencyInfo(soAdjust.AdjdCuryInfoID);
              PXLongOperation.SetCustomInfoPersistent((object) new PaymentTransactionGraph<ARPaymentEntry, PX.Objects.AR.ARPayment>.LongOperationWarning("CuryUnappliedBal", (PXSetPropertyException) new PXSetPropertyException<PX.Objects.AR.ARPayment.curyUnappliedBal>("The {4} {0} amount cannot be applied to the {2} {1} document whose unpaid balance is {5} {3}.", PXErrorLevel.Warning, new object[6]
              {
                (object) payment.OrigDocAppliedAmount,
                (object) soAdjust.AdjdOrderType,
                (object) soAdjust.AdjdOrderNbr,
                (object) soAdjust.CuryDocBal,
                (object) payment.CuryID,
                (object) currencyInfo.CuryID
              })));
            }
          }
          else
          {
            soAdjust.CuryAdjgAmt = payment.OrigDocAppliedAmount;
            applyTabExtension?.SOAdjustments.Update(soAdjust);
          }
        }
      }
    }
    IEnumerable<ARAdjust> source2 = graph.Adjustments.Select().AsEnumerable<PXResult<ARAdjust>>().Select<PXResult<ARAdjust>, ARAdjust>((Func<PXResult<ARAdjust>, ARAdjust>) (a => a.GetItem<ARAdjust>()));
    if (!source2.Any<ARAdjust>())
      return;
    ARAdjust arAdjust = source2.Where<ARAdjust>((Func<ARAdjust, bool>) (a => a.AdjdDocType == payment.TransactionOrigDocType && a.AdjdRefNbr == payment.TransactionOrigDocRefNbr)).SingleOrDefault<ARAdjust>();
    nullable2 = payment.OrigDocAppliedAmount;
    if (!nullable2.HasValue && arAdjust != null)
    {
      Payment payment2 = payment;
      curyAdjgAmt = arAdjust.CuryAdjgAmt;
      Decimal? curyDocBalIncrease = payment.CuryDocBalIncrease;
      nullable2 = curyAdjgAmt.HasValue & curyDocBalIncrease.HasValue ? new Decimal?(curyAdjgAmt.GetValueOrDefault() + curyDocBalIncrease.GetValueOrDefault()) : new Decimal?();
      nullable1 = payment.CuryDocBal;
      Decimal? nullable4 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      payment2.OrigDocAppliedAmount = nullable4;
    }
    if (arAdjust == null)
      return;
    nullable1 = arAdjust.CuryAdjgAmt;
    nullable2 = payment.OrigDocAppliedAmount;
    if (!(nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue))
      return;
    PX.Objects.AR.ARInvoice arInvoice = PX.Objects.AR.ARInvoice.PK.Find((PXGraph) graph, arAdjust.AdjdDocType, arAdjust.AdjdRefNbr);
    if (arInvoice == null)
      return;
    nullable2 = arInvoice.CuryUnpaidBalance;
    Decimal? docAppliedAmount1 = payment.OrigDocAppliedAmount;
    curyAdjgAmt = arAdjust.CuryAdjgAmt;
    nullable1 = docAppliedAmount1.HasValue & curyAdjgAmt.HasValue ? new Decimal?(docAppliedAmount1.GetValueOrDefault() - curyAdjgAmt.GetValueOrDefault()) : new Decimal?();
    if (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
    {
      if (!PXLongOperation.IsLongOperationContext())
        return;
      PXLongOperation.SetCustomInfoPersistent((object) new PaymentTransactionGraph<ARPaymentEntry, PX.Objects.AR.ARPayment>.LongOperationWarning("CuryUnappliedBal", (PXSetPropertyException) new PXSetPropertyException<PX.Objects.AR.ARPayment.curyUnappliedBal>("The {4} {0} amount cannot be applied to the {2} {1} document whose unpaid balance is {5} {3}.", PXErrorLevel.Warning, new object[6]
      {
        (object) payment.OrigDocAppliedAmount,
        (object) arAdjust.AdjdDocType,
        (object) arAdjust.AdjdRefNbr,
        (object) arInvoice.CuryUnpaidBalance,
        (object) payment.CuryID,
        (object) arAdjust.AdjdCuryID
      })));
    }
    else
    {
      arAdjust.CuryAdjgAmt = payment.OrigDocAppliedAmount;
      graph.Adjustments.Update(arAdjust);
    }
  }

  private bool ProcessAdjustments(ARPaymentEntry graph, PX.Objects.AR.ExternalTransaction extTran)
  {
    PXResultset<SOAdjust> pxResultset1 = graph.GetOrdersToApplyTabExtension()?.SOAdjustments.Select();
    PXResultset<ARAdjust> pxResultset2 = graph.Adjustments.Select();
    bool flag = false;
    Decimal? nullable1 = new Decimal?(0M);
    // ISSUE: explicit non-virtual call
    if (pxResultset1 != null && __nonvirtual (pxResultset1.Count) > 1 || pxResultset2.Count > 1)
    {
      if (pxResultset1 != null)
      {
        foreach (PXResult<SOAdjust> pxResult in pxResultset1)
        {
          SOAdjust soAdjust = (SOAdjust) pxResult;
          Decimal? nullable2 = nullable1;
          Decimal? curyAdjgAmt = soAdjust.CuryAdjgAmt;
          nullable1 = nullable2.HasValue & curyAdjgAmt.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + curyAdjgAmt.GetValueOrDefault()) : new Decimal?();
        }
      }
      foreach (PXResult<ARAdjust> pxResult in pxResultset2)
      {
        ARAdjust arAdjust = (ARAdjust) pxResult;
        Decimal? nullable3 = nullable1;
        Decimal? curyAdjgAmt = arAdjust.CuryAdjgAmt;
        nullable1 = nullable3.HasValue & curyAdjgAmt.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + curyAdjgAmt.GetValueOrDefault()) : new Decimal?();
      }
      Decimal? amount = extTran.Amount;
      Decimal? nullable4 = nullable1;
      return amount.GetValueOrDefault() >= nullable4.GetValueOrDefault() & amount.HasValue & nullable4.HasValue;
    }
    SOAdjust soAdjust1 = (SOAdjust) pxResultset1;
    ARAdjust arAdjust1 = (ARAdjust) pxResultset2;
    if (soAdjust1 != null && arAdjust1 != null && arAdjust1.AdjdOrderType != soAdjust1.AdjdOrderType && arAdjust1.AdjdOrderNbr != soAdjust1.AdjdOrderNbr)
    {
      Decimal? curyAdjgAmt1 = soAdjust1.CuryAdjgAmt;
      Decimal? curyAdjgAmt2 = arAdjust1.CuryAdjgAmt;
      Decimal? nullable5 = curyAdjgAmt1.HasValue & curyAdjgAmt2.HasValue ? new Decimal?(curyAdjgAmt1.GetValueOrDefault() + curyAdjgAmt2.GetValueOrDefault()) : new Decimal?();
      Decimal? amount = extTran.Amount;
      Decimal? nullable6 = nullable5;
      return amount.GetValueOrDefault() >= nullable6.GetValueOrDefault() & amount.HasValue & nullable6.HasValue;
    }
    Decimal? nullable7;
    Decimal? nullable8;
    if (soAdjust1 != null)
    {
      nullable7 = soAdjust1.CuryAdjgBilledAmt;
      Decimal num = 0M;
      if (nullable7.GetValueOrDefault() > num & nullable7.HasValue)
      {
        nullable7 = soAdjust1.CuryOrigAdjgAmt;
        Decimal? curyAdjgBilledAmt = soAdjust1.CuryAdjgBilledAmt;
        if (!(nullable7.GetValueOrDefault() == curyAdjgBilledAmt.GetValueOrDefault() & nullable7.HasValue == curyAdjgBilledAmt.HasValue))
          return false;
      }
      nullable8 = soAdjust1.CuryAdjgAmt;
      nullable7 = extTran.Amount;
      if (nullable8.GetValueOrDefault() > nullable7.GetValueOrDefault() & nullable8.HasValue & nullable7.HasValue)
      {
        soAdjust1.CuryAdjgAmt = extTran.Amount;
        graph.GetOrdersToApplyTabExtension(true).SOAdjustments.Update(soAdjust1);
        flag = true;
      }
    }
    if (arAdjust1 != null && !flag)
    {
      nullable7 = arAdjust1.CuryAdjgAmt;
      nullable8 = extTran.Amount;
      if (nullable7.GetValueOrDefault() > nullable8.GetValueOrDefault() & nullable7.HasValue & nullable8.HasValue)
      {
        arAdjust1.CuryAdjgAmt = extTran.Amount;
        graph.Adjustments.Update(arAdjust1);
      }
    }
    return true;
  }

  private bool NeedRelease(PX.Objects.AR.ARPayment doc)
  {
    bool? released = doc.Released;
    bool flag1 = false;
    if (released.GetValueOrDefault() == flag1 & released.HasValue)
    {
      bool? hold = doc.Hold;
      bool flag2 = false;
      if (hold.GetValueOrDefault() == flag2 & hold.HasValue)
        return CCProcessingHelper.IntegratedProcessingActivated(this.Graph.arsetup.Current);
    }
    return false;
  }

  public void CheckForHeldForReviewStatusAfterProc(
    ARPaymentEntry paymentEntry,
    CCTranType procTran,
    bool success)
  {
    if (!success)
      return;
    PX.Objects.AR.ARPayment current = paymentEntry.Document.Current;
    PXResultset<PX.Objects.AR.ExternalTransaction> resultSet = new PXSelect<PX.Objects.AR.ExternalTransaction, Where<PX.Objects.AR.ExternalTransaction.docType, Equal<Required<PX.Objects.AR.ExternalTransaction.docType>>, And<PX.Objects.AR.ExternalTransaction.refNbr, Equal<Required<PX.Objects.AR.ExternalTransaction.refNbr>>>>, PX.Data.OrderBy<Desc<PX.Objects.AR.ExternalTransaction.transactionID>>>((PXGraph) paymentEntry).Select((object) current.DocType, (object) current.RefNbr);
    if (ExternalTranHelper.GetActiveTransactionState((PXGraph) paymentEntry, (IEnumerable<IExternalTransaction>) resultSet.RowCast<PX.Objects.AR.ExternalTransaction>()).IsOpenForReview)
      throw new PXSetPropertyException("The transaction is held for review by the processing center. Use the processing center interface to approve or reject the transaction.", PXErrorLevel.RowWarning);
  }

  public void ReleaseDocument(ARPaymentEntry paymentGraph, CCTranType tranType, bool success)
  {
    if (!(paymentGraph.Document.Current != null & success))
      return;
    PX.Objects.AR.ExternalTransaction processedExternalTran = this.GetLastProcessedExternalTran(paymentGraph);
    if (processedExternalTran == null)
      return;
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) paymentGraph, (IExternalTransaction) processedExternalTran);
    if (transactionState.IsDeclined || transactionState.IsOpenForReview || transactionState.IsExpired || transactionState.SyncFailed || transactionState.NeedSync)
      return;
    this.PersistData();
    PX.Objects.AR.ARPayment current = paymentGraph.Document.Current;
    PaymentTransactionGraph<ARPaymentEntry, PX.Objects.AR.ARPayment>.ReleaseARDocument((IBqlTable) current);
    paymentGraph.Document.Current = PrimaryKeyOf<PX.Objects.AR.ARPayment>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<PX.Objects.AR.ARPayment.docType, PX.Objects.AR.ARPayment.refNbr>>.Find((PXGraph) paymentGraph, current);
    if (!WebConfig.IsClusterEnabled)
      return;
    PXLongOperation.SetCustomInfo((object) new ClearTransactionCache());
  }

  public void UpdateARPaymentAndSetWarning(
    ARPaymentEntry paymentGraph,
    CCTranType tranType,
    bool success)
  {
    PX.Objects.AR.ARPayment current = paymentGraph.Document.Current;
    if (!success)
      return;
    bool? released = current.Released;
    bool flag = false;
    if (!(released.GetValueOrDefault() == flag & released.HasValue))
      return;
    IExternalTransaction processedExternalTran = (IExternalTransaction) this.GetLastProcessedExternalTran(paymentGraph);
    if (processedExternalTran == null)
      return;
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) paymentGraph, processedExternalTran);
    if (processedExternalTran == null)
      return;
    int? transactionId = processedExternalTran.TransactionID;
    int? externalTransactionId = current.CCActualExternalTransactionID;
    if (!(transactionId.GetValueOrDefault() == externalTransactionId.GetValueOrDefault() & transactionId.HasValue == externalTransactionId.HasValue))
      return;
    if (transactionState.IsActive)
    {
      System.DateTime? nullable1 = current.AdjDate;
      System.DateTime t1 = nullable1.Value;
      nullable1 = processedExternalTran.LastActivityDate;
      System.DateTime date = nullable1.Value.Date;
      int num = System.DateTime.Compare(t1, date);
      nullable1 = current.AdjDate;
      if (nullable1.HasValue && num != 0 && PXLongOperation.IsLongOperationContext() && !(PXLongOperation.GetCustomInfo() is PXProcessingInfo))
        PXLongOperation.SetCustomInfoPersistent((object) new PaymentTransactionGraph<ARPaymentEntry, PX.Objects.AR.ARPayment>.LongOperationWarning("AdjDate", (PXSetPropertyException) new PXSetPropertyException<PX.Objects.AR.ARPayment.adjDate>("Application date was changed to date of card transaction", PXErrorLevel.Warning)));
      PX.Objects.AR.ARPayment arPayment1 = current;
      nullable1 = processedExternalTran.LastActivityDate;
      System.DateTime? nullable2 = new System.DateTime?(nullable1.Value.Date);
      arPayment1.DocDate = nullable2;
      PX.Objects.AR.ARPayment arPayment2 = current;
      nullable1 = processedExternalTran.LastActivityDate;
      System.DateTime? nullable3 = new System.DateTime?(nullable1.Value.Date);
      arPayment2.AdjDate = nullable3;
    }
    this.SetExtRefNbrValue(paymentGraph, current, processedExternalTran, transactionState);
    paymentGraph.Document.Update(current);
  }

  public void UpdateARPayment(ARPaymentEntry paymentGraph, CCTranType tranType, bool success)
  {
    PX.Objects.AR.ARPayment current = paymentGraph.Document.Current;
    if (!success)
      return;
    bool? released = current.Released;
    bool flag = false;
    if (!(released.GetValueOrDefault() == flag & released.HasValue))
      return;
    IExternalTransaction processedExternalTran = (IExternalTransaction) this.GetLastProcessedExternalTran(paymentGraph);
    if (processedExternalTran == null)
      return;
    int? transactionId = processedExternalTran.TransactionID;
    int? externalTransactionId = current.CCActualExternalTransactionID;
    if (!(transactionId.GetValueOrDefault() == externalTransactionId.GetValueOrDefault() & transactionId.HasValue == externalTransactionId.HasValue))
      return;
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) paymentGraph, processedExternalTran);
    PX.Objects.AR.ARPayment arPayment1 = current;
    System.DateTime? lastActivityDate = processedExternalTran.LastActivityDate;
    System.DateTime? nullable1 = new System.DateTime?(lastActivityDate.Value.Date);
    arPayment1.DocDate = nullable1;
    PX.Objects.AR.ARPayment arPayment2 = current;
    lastActivityDate = processedExternalTran.LastActivityDate;
    System.DateTime? nullable2 = new System.DateTime?(lastActivityDate.Value.Date);
    arPayment2.AdjDate = nullable2;
    this.SetExtRefNbrValue(paymentGraph, current, processedExternalTran, transactionState);
    paymentGraph.Document.Update(current);
  }

  public void ChangeDocProcessingStatus(
    ARPaymentEntry paymentGraph,
    CCTranType tranType,
    bool success)
  {
    PX.Objects.AR.ExternalTransaction processedExternalTran = this.GetLastProcessedExternalTran(paymentGraph);
    PX.Objects.AR.ARPayment current = paymentGraph.Document.Current;
    if (processedExternalTran == null)
      return;
    this.DeactivateExpiredTrans(paymentGraph);
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) paymentGraph, (IExternalTransaction) processedExternalTran);
    this.ChangeCaptureFailedFlag(transactionState, current);
    this.ChangeUserAttentionFlag(transactionState, paymentGraph);
    PX.Objects.AR.ARPayment arPayment = this.SyncActualExternalTransation(paymentGraph, current);
    if (success)
    {
      bool flag1 = true;
      bool flag2 = !transactionState.IsOpenForReview && !transactionState.NeedSync && !transactionState.CreateProfile;
      if (((!this.PaymentDocType(arPayment) ? 0 : (transactionState.IsCaptured ? 1 : 0)) & (flag2 ? 1 : 0)) != 0)
        flag1 = false;
      if (((arPayment.DocType == "RPM" || arPayment.DocType == "REF" ? (transactionState.IsVoided ? 1 : (transactionState.IsRefunded ? 1 : 0)) : 0) & (flag2 ? 1 : 0)) != 0)
        flag1 = false;
      if (arPayment.Released.GetValueOrDefault())
        flag1 = false;
      this.ChangeDocProcessingFlags(transactionState, arPayment, tranType);
      arPayment.PendingProcessing = new bool?(flag1);
    }
    this.ChangeOriginDocProcessingStatus(paymentGraph, tranType, success);
    paymentGraph.Document.Update(arPayment);
  }

  private bool CreateVoidDocument(ARPaymentEntry graph, bool success)
  {
    CCTranType tranType = CCTranType.VoidOrCredit;
    PX.Objects.AR.ARPayment current1 = this.Graph.Document.Current;
    PX.Objects.AR.ExternalTransaction processedExternalTran = this.GetLastProcessedExternalTran(graph);
    if (processedExternalTran != null && this.NeedSyncContext && success)
    {
      bool? released = current1.Released;
      bool flag = false;
      if (!(released.GetValueOrDefault() == flag & released.HasValue) && this.PaymentDocType(current1))
      {
        ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) graph, (IExternalTransaction) processedExternalTran);
        if (!transactionState.IsRefunded && !transactionState.IsVoided)
          return false;
        ARPaymentEntry graphByDocTypeRefNbr = this.GetGraphByDocTypeRefNbr(current1.DocType, current1.RefNbr);
        PXAdapter adapter = this.CreateAdapter(graphByDocTypeRefNbr, current1);
        try
        {
          graphByDocTypeRefNbr.VoidCheck(adapter);
        }
        catch (PXRedirectRequiredException ex)
        {
        }
        graphByDocTypeRefNbr.Save.Press();
        PX.Objects.AR.ARPayment current2 = graphByDocTypeRefNbr.Document.Current;
        if (transactionState.IsRefunded)
          this.MoveTranToAnotherDoc(graphByDocTypeRefNbr, processedExternalTran);
        if (transactionState.IsVoided && current2.DocType != processedExternalTran.DocType)
          this.UpdateVoidDocTypeRefNbr(graphByDocTypeRefNbr, processedExternalTran);
        this.ChangeDocProcessingStatus(graphByDocTypeRefNbr, tranType, true);
        this.UpdateARPaymentAndSetWarning(graphByDocTypeRefNbr, tranType, true);
        graphByDocTypeRefNbr.Save.Press();
        if (this.ReleaseDoc && this.NeedRelease(current2) && ARPaymentType.VoidAppl(current2.DocType))
          this.ReleaseDocument(graphByDocTypeRefNbr, tranType, true);
        graph.Cancel.Press();
        graph.Document.Current = (PX.Objects.AR.ARPayment) PXSelectBase<PX.Objects.AR.ARPayment, PXSelect<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARPayment.docType, Equal<Required<PX.Objects.AR.ARPayment.docType>>, And<PX.Objects.AR.ARPayment.refNbr, Equal<Required<PX.Objects.AR.ARPayment.refNbr>>>>>.Config>.Select((PXGraph) graph, (object) current1.DocType, (object) current1.RefNbr);
        return true;
      }
    }
    return false;
  }

  private bool VoidOriginalPayment(ARPaymentEntry graph, bool success)
  {
    PX.Objects.AR.ARPayment current = graph.Document.Current;
    PX.Objects.AR.ExternalTransaction processedExternalTran = this.GetLastProcessedExternalTran(graph);
    if (processedExternalTran != null && this.NeedSyncContext && success)
    {
      bool? nullable = current.Released;
      if (!nullable.GetValueOrDefault())
      {
        nullable = current.Voided;
        if (!nullable.GetValueOrDefault() && this.PaymentDocType(current) && ExternalTranHelper.GetTransactionState((PXGraph) graph, (IExternalTransaction) processedExternalTran).IsVoided)
        {
          PXAdapter adapter = this.CreateAdapter(graph, current);
          graph.VoidCheck(adapter);
          return true;
        }
      }
    }
    return false;
  }

  private void UpdateVoidDocTypeRefNbr(ARPaymentEntry newGraph, PX.Objects.AR.ExternalTransaction extTran)
  {
    PX.Objects.AR.ExternalTransaction externalTransaction = newGraph.ExternalTran.Select().RowCast<PX.Objects.AR.ExternalTransaction>().Where<PX.Objects.AR.ExternalTransaction>((Func<PX.Objects.AR.ExternalTransaction, bool>) (i =>
    {
      int? transactionId1 = i.TransactionID;
      int? transactionId2 = extTran.TransactionID;
      return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
    })).FirstOrDefault<PX.Objects.AR.ExternalTransaction>();
    CCProcTran ccProcTran = newGraph.ccProcTran.Select().RowCast<CCProcTran>().Where<CCProcTran>((Func<CCProcTran, bool>) (i =>
    {
      int? transactionId3 = i.TransactionID;
      int? transactionId4 = extTran.TransactionID;
      return transactionId3.GetValueOrDefault() == transactionId4.GetValueOrDefault() & transactionId3.HasValue == transactionId4.HasValue;
    })).FirstOrDefault<CCProcTran>();
    PX.Objects.AR.ARPayment current = newGraph.Document.Current;
    if (externalTransaction == null || ccProcTran == null)
      return;
    externalTransaction.VoidDocType = current.DocType;
    externalTransaction.VoidRefNbr = current.RefNbr;
    ccProcTran.DocType = current.DocType;
    ccProcTran.RefNbr = current.RefNbr;
    newGraph.ExternalTran.Update(externalTransaction);
    newGraph.ccProcTran.Update(ccProcTran);
  }

  private void MoveTranToAnotherDoc(ARPaymentEntry graph, PX.Objects.AR.ExternalTransaction extTran)
  {
    PX.Objects.AR.ARPayment current = graph.Document.Current;
    PXDatabase.Update<PX.Objects.AR.ExternalTransaction>((PXDataFieldParam) new PXDataFieldAssign("DocType", (object) current.DocType), (PXDataFieldParam) new PXDataFieldAssign("RefNbr", (object) current.RefNbr), (PXDataFieldParam) new PXDataFieldRestrict("TransactionID", PXDbType.Int, new int?(4), (object) extTran.TransactionID, PXComp.EQ));
    PXDatabase.Update<CCProcTran>((PXDataFieldParam) new PXDataFieldAssign("DocType", (object) current.DocType), (PXDataFieldParam) new PXDataFieldAssign("RefNbr", (object) current.RefNbr), (PXDataFieldParam) new PXDataFieldRestrict("TransactionID", PXDbType.Int, new int?(4), (object) extTran.TransactionID, PXComp.EQ));
    graph.ExternalTran.Cache.Clear();
    graph.ExternalTran.Cache.ClearQueryCache();
  }

  private void UpdateDocCleared(ARPaymentEntry graph)
  {
    IExternalTransaction processedExternalTran = (IExternalTransaction) this.GetLastProcessedExternalTran(graph);
    if (processedExternalTran == null)
      return;
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) graph, processedExternalTran);
    if ((transactionState != null ? (transactionState.IsCaptured ? 1 : 0) : 0) == 0)
      return;
    PX.Objects.AR.ARPayment current = graph.Document.Current;
    bool? settlementBatches = CCProcessingCenter.PK.Find((PXGraph) graph, current.ProcessingCenterID).ImportSettlementBatches;
    bool flag = false;
    if (!(settlementBatches.GetValueOrDefault() == flag & settlementBatches.HasValue))
      return;
    current.Cleared = new bool?(true);
    current.ClearDate = new System.DateTime?(processedExternalTran.LastActivityDate.Value.Date);
  }

  private void ChangeOriginDocProcessingStatus(
    ARPaymentEntry paymentGraph,
    CCTranType tranType,
    bool success)
  {
    IExternalTransaction processedExternalTran1 = (IExternalTransaction) this.GetLastProcessedExternalTran(paymentGraph);
    PX.Objects.AR.ARPayment current1 = paymentGraph.Document.Current;
    if (processedExternalTran1 == null)
      return;
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) paymentGraph, processedExternalTran1);
    ARPaymentEntry graphWithOriginDoc = this.GetGraphWithOriginDoc(paymentGraph, tranType);
    if (graphWithOriginDoc == null)
      return;
    PX.Objects.AR.ExternalTransaction processedExternalTran2 = this.GetLastProcessedExternalTran(graphWithOriginDoc);
    if (processedExternalTran2 == null)
      return;
    PX.Objects.AR.ARPayment current2 = graphWithOriginDoc.Document.Current;
    int? transactionId1 = processedExternalTran2.TransactionID;
    int? transactionId2 = processedExternalTran1.TransactionID;
    if (transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue)
    {
      this.ChangeCaptureFailedFlag(transactionState, current2);
      if (success)
      {
        if (current2.Released.GetValueOrDefault())
          current2.IsCCUserAttention = new bool?(false);
        this.ChangeDocProcessingFlags(transactionState, current2, tranType);
      }
    }
    paymentGraph.Caches[typeof (PX.Objects.AR.ARPayment)].Update((object) current2);
  }

  private void UpdateExtRefNbrARPayment(ARPaymentEntry graph, CCTranType tranType, bool success)
  {
    ARPaymentEntry graph1 = graph;
    PX.Objects.AR.ARPayment current = graph1.Document.Current;
    if (!success)
      return;
    bool? released = current.Released;
    bool flag = false;
    if (!(released.GetValueOrDefault() == flag & released.HasValue))
      return;
    IExternalTransaction processedExternalTran = (IExternalTransaction) this.GetLastProcessedExternalTran(graph);
    if (processedExternalTran == null)
      return;
    ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) graph1, processedExternalTran);
    if (processedExternalTran != null)
      this.SetExtRefNbrValue(graph1, current, processedExternalTran, transactionState);
    graph1.Document.Update(current);
  }

  private void SetExtRefNbrValue(
    ARPaymentEntry graph,
    PX.Objects.AR.ARPayment doc,
    IExternalTransaction currTran,
    ExternalTransactionState state)
  {
    if (!state.IsActive && (!EnumerableExtensions.IsIn<string>(doc.DocType, "REF", "PPM", "PMT") || !state.IsVoided || !string.IsNullOrEmpty(doc.ExtRefNbr)))
      return;
    graph.Document.Cache.SetValue<PX.Objects.AR.ARPayment.extRefNbr>((object) doc, (object) currTran.TranNumber);
  }

  private void DeactivateExpiredTrans(ARPaymentEntry graph)
  {
    PXCache cache = graph.Document.Cache;
    foreach (PX.Objects.AR.ExternalTransaction extTran in graph.ExternalTran.Select().RowCast<PX.Objects.AR.ExternalTransaction>().Where<PX.Objects.AR.ExternalTransaction>((Func<PX.Objects.AR.ExternalTransaction, bool>) (i => ExternalTranHelper.IsExpired((IExternalTransaction) i) && i.Active.GetValueOrDefault())))
    {
      ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState((PXGraph) graph, (IExternalTransaction) extTran);
      extTran.Active = new bool?(false);
      extTran.ProcStatus = ExtTransactionProcStatusCode.GetProcStatusStrByProcessingStatus(transactionState.ProcessingStatus);
      graph.ExternalTran.Update(extTran);
    }
  }

  private void ChangeCaptureFailedFlag(ExternalTransactionState state, PX.Objects.AR.ARPayment doc)
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

  private void ChangeUserAttentionFlag(ARPaymentEntry graph)
  {
    IExternalTransaction processedExternalTran = (IExternalTransaction) this.GetLastProcessedExternalTran(graph);
    if (processedExternalTran == null)
      return;
    this.ChangeUserAttentionFlag(ExternalTranHelper.GetTransactionState((PXGraph) graph, processedExternalTran), graph);
  }

  private void ChangeUserAttentionFlag(ExternalTransactionState state, ARPaymentEntry graph)
  {
    PX.Objects.AR.ARPayment doc = graph.Document.Current;
    bool flag = false;
    if ((state.ProcessingStatus == ProcessingStatus.AuthorizeDecline || state.ProcessingStatus == ProcessingStatus.AuthorizeFail ? (doc.IsCCUserAttention.GetValueOrDefault() ? 1 : 0) : 0) != 0 || state.IsOpenForReview || state.SyncFailed || doc.IsCCCaptureFailed.GetValueOrDefault() || state.ProcessingStatus == ProcessingStatus.AuthorizeIncreaseFail || state.ProcessingStatus == ProcessingStatus.VoidFail || state.ProcessingStatus == ProcessingStatus.VoidDecline)
      flag = true;
    int num = graph.ExternalTran.Select().RowCast<PX.Objects.AR.ExternalTransaction>().Where<PX.Objects.AR.ExternalTransaction>((Func<PX.Objects.AR.ExternalTransaction, bool>) (i => i.Active.GetValueOrDefault() && doc.DocType == i.DocType && doc.RefNbr == i.RefNbr)).Count<PX.Objects.AR.ExternalTransaction>();
    if (num > 1)
      flag = true;
    int? pmInstanceId = doc.PMInstanceID;
    int? newPaymentProfile = PaymentTranExtConstants.NewPaymentProfile;
    if (pmInstanceId.GetValueOrDefault() == newPaymentProfile.GetValueOrDefault() & pmInstanceId.HasValue == newPaymentProfile.HasValue && num == 0 && this.PaymentDocType(doc))
      flag = true;
    doc.IsCCUserAttention = new bool?(flag);
  }

  private bool CheckCaptureFailedExists(ExternalTransactionState state)
  {
    bool flag = false;
    if (CCProcTranHelper.HasCaptureFailed((IEnumerable<ICCPaymentTransaction>) this.GetPaymentProcessingRepository().GetCCProcTranByTranID(state.ExternalTransaction.TransactionID)))
      flag = true;
    return flag;
  }

  private void ChangeDocProcessingFlags(
    ExternalTransactionState tranState,
    PX.Objects.AR.ARPayment doc,
    CCTranType tranType)
  {
    int? transactionId = (int?) tranState.ExternalTransaction?.TransactionID;
    int? externalTransactionId = doc.CCActualExternalTransactionID;
    if (!(transactionId.GetValueOrDefault() == externalTransactionId.GetValueOrDefault() & transactionId.HasValue == externalTransactionId.HasValue))
      return;
    PX.Objects.AR.ARPayment arPayment1 = doc;
    PX.Objects.AR.ARPayment arPayment2 = doc;
    PX.Objects.AR.ARPayment arPayment3 = doc;
    bool? nullable1 = new bool?(false);
    bool? nullable2 = nullable1;
    arPayment3.IsCCRefunded = nullable2;
    bool? nullable3;
    bool? nullable4 = nullable3 = nullable1;
    arPayment2.IsCCCaptured = nullable3;
    bool? nullable5 = nullable4;
    arPayment1.IsCCAuthorized = nullable5;
    if (tranState.IsDeclined || tranState.IsOpenForReview || tranState.SyncFailed || ExternalTranHelper.IsExpired(tranState.ExternalTransaction))
      return;
    switch (tranType)
    {
      case CCTranType.AuthorizeAndCapture:
        doc.IsCCCaptured = new bool?(true);
        break;
      case CCTranType.AuthorizeOnly:
      case CCTranType.IncreaseAuthorizedAmount:
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
    if (tranType == CCTranType.VoidOrCredit && tranState.IsRefunded)
      doc.IsCCRefunded = new bool?(true);
    if (!this.PaymentDocType(doc) || !tranState.IsCaptured)
      return;
    doc.IsCCCaptured = new bool?(true);
  }

  private PXAdapter CreateAdapter(ARPaymentEntry graph, PX.Objects.AR.ARPayment doc)
  {
    return new PXAdapter((PX.Data.PXView) new PX.Data.PXView.Dummy((PXGraph) graph, graph.Document.View.BqlSelect, new List<object>()
    {
      (object) doc
    }));
  }

  private ARPaymentEntry GetGraphWithOriginDoc(ARPaymentEntry graph, CCTranType tranType)
  {
    if (this.graphWithOriginDoc != null)
      return this.graphWithOriginDoc;
    IExternalTransaction processedExternalTran = (IExternalTransaction) this.GetLastProcessedExternalTran(graph);
    PX.Objects.AR.ARPayment current = graph.Document.Current;
    if (processedExternalTran != null && tranType == CCTranType.VoidOrCredit && (processedExternalTran.DocType == current.OrigDocType && processedExternalTran.RefNbr == current.OrigRefNbr || processedExternalTran.VoidDocType == current.DocType && processedExternalTran.VoidRefNbr == current.RefNbr) && (processedExternalTran.DocType == "PMT" || processedExternalTran.DocType == "PPM"))
      this.graphWithOriginDoc = this.GetGraphByDocTypeRefNbr(processedExternalTran.DocType, processedExternalTran.RefNbr);
    return this.graphWithOriginDoc;
  }

  private ARPaymentEntry GetGraphByDocTypeRefNbr(string docType, string refNbr)
  {
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    instance.RowSelecting.RemoveHandler<PX.Objects.AR.ARPayment>(new PXRowSelecting(instance.ARPayment_RowSelecting));
    instance.Document.Current = (PX.Objects.AR.ARPayment) PXSelectBase<PX.Objects.AR.ARPayment, PXSelect<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARPayment.docType, Equal<Required<PX.Objects.AR.ARPayment.docType>>, And<PX.Objects.AR.ARPayment.refNbr, Equal<Required<PX.Objects.AR.ARPayment.refNbr>>>>>.Config>.Select((PXGraph) instance, (object) docType, (object) refNbr);
    return instance;
  }

  private PX.Objects.AR.ExternalTransaction GetLastProcessedExternalTran(ARPaymentEntry graph)
  {
    IEnumerable<PX.Objects.AR.ExternalTransaction> externalTransactions = graph.ExternalTran.Select().RowCast<PX.Objects.AR.ExternalTransaction>();
    if (externalTransactions.Count<PX.Objects.AR.ExternalTransaction>() == 0)
      return (PX.Objects.AR.ExternalTransaction) null;
    IEnumerable<CCProcTran> trans = graph.ccProcTran.Select().RowCast<CCProcTran>().Where<CCProcTran>((Func<CCProcTran, bool>) (i => i.ProcStatus != "OPN"));
    IExternalTransaction extTran = ExternalTranHelper.GetLastProcessedExtTran((IEnumerable<IExternalTransaction>) externalTransactions, (IEnumerable<ICCPaymentTransaction>) trans);
    return extTran == null ? (PX.Objects.AR.ExternalTransaction) null : externalTransactions.Where<PX.Objects.AR.ExternalTransaction>((Func<PX.Objects.AR.ExternalTransaction, bool>) (i =>
    {
      int? transactionId1 = i.TransactionID;
      int? transactionId2 = extTran.TransactionID;
      return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
    })).FirstOrDefault<PX.Objects.AR.ExternalTransaction>();
  }

  public override void PersistData()
  {
    PX.Objects.AR.ARPayment current = this.Graph?.Document.Current;
    if (current != null && this.Graph.Document.Cache.GetStatus((object) current) != PXEntryStatus.Notchanged)
      this.Graph.Save.Press();
    this.RestoreCopy();
  }

  public override PXGraph GetGraph() => (PXGraph) this.Graph;

  protected virtual ARPaymentEntry CreateGraphIfNeeded(IBqlTable table)
  {
    if (this.Graph == null)
    {
      PX.Objects.AR.ARPayment arPayment = table as PX.Objects.AR.ARPayment;
      this.Graph = this.GetGraphByDocTypeRefNbr(arPayment.DocType, arPayment.RefNbr);
      this.Graph.Document.Update(arPayment);
    }
    return this.Graph;
  }

  protected virtual ICCPaymentProcessingRepository GetPaymentProcessingRepository()
  {
    return (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository((PXGraph) this.Graph);
  }

  private bool CheckImportDeactivatedTran(ARPaymentEntry paymentGraph)
  {
    PX.Objects.AR.ExternalTransaction externalTransaction = paymentGraph.ExternalTran.SelectSingle();
    if (externalTransaction != null && externalTransaction.NeedSync.GetValueOrDefault())
    {
      bool? active = externalTransaction.Active;
      bool flag = false;
      if (active.GetValueOrDefault() == flag & active.HasValue)
      {
        this.importDeactivatedTran = true;
        goto label_4;
      }
    }
    this.importDeactivatedTran = false;
label_4:
    return this.importDeactivatedTran;
  }

  private PX.Objects.AR.ARPayment SyncActualExternalTransation(
    ARPaymentEntry paymentGraph,
    PX.Objects.AR.ARPayment payment)
  {
    PX.Objects.AR.ExternalTransaction processedExternalTran = this.GetLastProcessedExternalTran(paymentGraph);
    if (processedExternalTran == null)
      return payment;
    PX.Objects.AR.ExternalTransaction extTran = (PX.Objects.AR.ExternalTransaction) paymentGraph.ExternalTran.Select().SingleOrDefault<PXResult<PX.Objects.AR.ExternalTransaction>>((Expression<Func<PXResult<PX.Objects.AR.ExternalTransaction>, bool>>) (t => ((PX.Objects.AR.ExternalTransaction) t).TransactionID == payment.CCActualExternalTransactionID));
    int? nullable = payment.CCActualExternalTransactionID;
    if (!nullable.HasValue || extTran == null)
    {
      payment.CCActualExternalTransactionID = processedExternalTran.TransactionID;
      return payment;
    }
    nullable = processedExternalTran.TransactionID;
    int? externalTransactionId = payment.CCActualExternalTransactionID;
    if (nullable.GetValueOrDefault() > externalTransactionId.GetValueOrDefault() & nullable.HasValue & externalTransactionId.HasValue)
    {
      ExternalTransactionState transactionState1 = ExternalTranHelper.GetTransactionState((PXGraph) paymentGraph, (IExternalTransaction) extTran);
      ExternalTransactionState transactionState2 = ExternalTranHelper.GetTransactionState((PXGraph) paymentGraph, (IExternalTransaction) processedExternalTran);
      if (!transactionState1.IsActive || transactionState2.IsActive)
        payment.CCActualExternalTransactionID = processedExternalTran.TransactionID;
    }
    return payment;
  }

  private void RestoreCopy()
  {
    PX.Objects.AR.ARPayment current = this.Graph?.Document.Current;
    if (current == null || this.inputTable == null)
      return;
    this.Graph.Document.Cache.RestoreCopy((object) this.inputTable, (object) current);
  }

  private bool PaymentDocType(PX.Objects.AR.ARPayment payment)
  {
    bool flag = false;
    string docType = payment.DocType;
    if (docType == "PMT" || docType == "PPM")
      flag = true;
    return flag;
  }

  private void UpdateDocReAuthFieldsAfterAuthorize(ARPaymentEntry paymentGraph)
  {
    PX.Objects.AR.ARPayment current = paymentGraph.Document.Current;
    ExternalTransactionState externalTransaction = this.GetStateOfLastExternalTransaction(paymentGraph);
    if ((externalTransaction != null ? (externalTransaction.IsPreAuthorized ? 1 : 0) : 0) != 0 || !current.CCReauthDate.HasValue)
      this.ExcludeFromReAuthProcess(paymentGraph, current);
    else
      this.HandleUnsuccessfulAttemptOfReauth(paymentGraph, current);
  }

  private void UpdateDocReAuthFieldsAfterCapture(ARPaymentEntry graph)
  {
    ExternalTransactionState externalTransaction = this.GetStateOfLastExternalTransaction(graph);
    if ((externalTransaction != null ? (externalTransaction.IsCaptured ? 1 : 0) : 0) == 0)
      return;
    PX.Objects.AR.ARPayment current = graph.Document.Current;
    this.ExcludeFromReAuthProcess(graph, current);
  }

  private void UpdateDocReAuthFieldsAfterVoid(ARPaymentEntry graph)
  {
    ExternalTransactionState externalTransaction = this.GetStateOfLastExternalTransaction(graph);
    if ((externalTransaction != null ? (externalTransaction.IsVoided ? 1 : 0) : 0) == 0)
      return;
    PX.Objects.AR.ARPayment current = graph.Document.Current;
    this.ExcludeFromReAuthProcess(graph, current);
  }

  private void UpdateDocReAuthFieldsAfterValidationByVoidForReAuth(ARPaymentEntry paymentGraph)
  {
    ExternalTransactionState externalTransaction = this.GetStateOfLastExternalTransaction(paymentGraph);
    if (externalTransaction == null || externalTransaction.IsActive || externalTransaction == null || externalTransaction.SyncFailed)
      return;
    PX.Objects.AR.ARPayment current = paymentGraph.Document.Current;
    this.IncludeToReAuthProcess(paymentGraph, current);
  }

  private void UpdateDocReAuthFieldsAfterVoidForReAuth(ARPaymentEntry paymentGraph)
  {
    ExternalTransactionState externalTransaction = this.GetStateOfLastExternalTransaction(paymentGraph);
    if ((externalTransaction != null ? (externalTransaction.IsVoided ? 1 : 0) : 0) == 0)
      return;
    PX.Objects.AR.ARPayment current = paymentGraph.Document.Current;
    this.IncludeToReAuthProcess(paymentGraph, current);
  }

  private ExternalTransactionState GetStateOfLastExternalTransaction(ARPaymentEntry payment)
  {
    IExternalTransaction processedExternalTran = (IExternalTransaction) this.GetLastProcessedExternalTran(payment);
    return processedExternalTran == null ? (ExternalTransactionState) null : ExternalTranHelper.GetTransactionState((PXGraph) payment, processedExternalTran);
  }

  private void HandleUnsuccessfulAttemptOfReauth(ARPaymentEntry paymentGraph, PX.Objects.AR.ARPayment payment)
  {
    CCPaymentProcessingRepository.GetCCPaymentProcessingRepository();
    CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find((PXGraph) paymentGraph, payment.ProcessingCenterID);
    PX.Objects.AR.ARPayment arPayment = payment;
    int? ccReauthTriesLeft1 = arPayment.CCReauthTriesLeft;
    arPayment.CCReauthTriesLeft = ccReauthTriesLeft1.HasValue ? new int?(ccReauthTriesLeft1.GetValueOrDefault() - 1) : new int?();
    int? ccReauthTriesLeft2 = payment.CCReauthTriesLeft;
    int num = 0;
    if (ccReauthTriesLeft2.GetValueOrDefault() > num & ccReauthTriesLeft2.HasValue)
    {
      payment.CCReauthDate = new System.DateTime?(PXTimeZoneInfo.Now.AddHours((double) processingCenter.ReauthRetryDelay.Value));
      paymentGraph.Document.Update(payment);
    }
    else
    {
      payment.IsCCUserAttention = new bool?(true);
      this.ExcludeFromReAuthProcess(paymentGraph, payment);
    }
  }

  private void ExcludeFromReAuthProcess(ARPaymentEntry paymentGraph, PX.Objects.AR.ARPayment payment)
  {
    payment.CCReauthDate = new System.DateTime?();
    payment.CCReauthTriesLeft = new int?(0);
    paymentGraph.Document.Update(payment);
  }

  private void IncludeToReAuthProcess(ARPaymentEntry paymentGraph, PX.Objects.AR.ARPayment payment)
  {
    CCPaymentProcessingRepository.GetCCPaymentProcessingRepository();
    CCProcessingCenter processingCenter = CCProcessingCenter.PK.Find((PXGraph) paymentGraph, payment.ProcessingCenterID);
    CCProcessingCenterPmntMethod centerPmntMethod = this.GetProcessingCenterPmntMethod(paymentGraph, payment);
    payment.CCReauthDate = new System.DateTime?(PXTimeZoneInfo.Now.AddHours((double) centerPmntMethod.ReauthDelay.Value));
    payment.CCReauthTriesLeft = processingCenter.ReauthRetryNbr;
    paymentGraph.Document.Update(payment);
  }

  private CCProcessingCenterPmntMethod GetProcessingCenterPmntMethod(
    ARPaymentEntry paymentGraph,
    PX.Objects.AR.ARPayment payment)
  {
    using (IEnumerator<PXResult<CCProcessingCenterPmntMethod>> enumerator = new FbqlSelect<SelectFromBase<CCProcessingCenterPmntMethod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<CCProcessingCenterPmntMethod.paymentMethodID, Equal<P.AsString>>>>>.And<BqlOperand<CCProcessingCenterPmntMethod.processingCenterID, IBqlString>.IsEqual<P.AsString>>>, CCProcessingCenterPmntMethod>.View((PXGraph) paymentGraph).Select((object) payment.PaymentMethodID, (object) payment.ProcessingCenterID).GetEnumerator())
    {
      if (enumerator.MoveNext())
        return (CCProcessingCenterPmntMethod) enumerator.Current;
    }
    return (CCProcessingCenterPmntMethod) null;
  }
}
