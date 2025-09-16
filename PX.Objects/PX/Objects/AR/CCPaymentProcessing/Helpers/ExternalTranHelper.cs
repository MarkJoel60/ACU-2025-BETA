// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Helpers.ExternalTranHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.AR.Repositories;
using PX.Objects.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Helpers;

public static class ExternalTranHelper
{
  public static bool HasTransactions(PXSelectBase<ExternalTransaction> extTrans)
  {
    return extTrans.Any<ExternalTransaction>();
  }

  public static IExternalTransaction GetActiveTransaction(PXSelectBase<ExternalTransaction> extTrans)
  {
    return ExternalTranHelper.GetActiveTransaction((IEnumerable<IExternalTransaction>) GraphHelper.RowCast<ExternalTransaction>((IEnumerable) extTrans.Select(Array.Empty<object>())));
  }

  public static IExternalTransaction GetDeactivatedNeedSyncTransaction(
    IEnumerable<IExternalTransaction> extTrans)
  {
    extTrans = (IEnumerable<IExternalTransaction>) extTrans.OrderByDescending<IExternalTransaction, int?>((Func<IExternalTransaction, int?>) (i => i.TransactionID));
    return extTrans.Where<IExternalTransaction>((Func<IExternalTransaction, bool>) (i =>
    {
      if (!i.NeedSync.GetValueOrDefault())
        return false;
      bool? active = i.Active;
      bool flag = false;
      return active.GetValueOrDefault() == flag & active.HasValue;
    })).FirstOrDefault<IExternalTransaction>();
  }

  public static IExternalTransaction GetActiveTransaction(IEnumerable<IExternalTransaction> extTrans)
  {
    extTrans = (IEnumerable<IExternalTransaction>) extTrans.OrderByDescending<IExternalTransaction, int?>((Func<IExternalTransaction, int?>) (i => i.TransactionID));
    return extTrans.Where<IExternalTransaction>((Func<IExternalTransaction, bool>) (i => i.Active.GetValueOrDefault())).FirstOrDefault<IExternalTransaction>();
  }

  public static bool HasSuccessfulTrans(PXSelectBase<ExternalTransaction> extTrans)
  {
    IExternalTransaction activeTransaction = ExternalTranHelper.GetActiveTransaction(extTrans);
    return activeTransaction != null && !ExternalTranHelper.IsExpired(activeTransaction);
  }

  public static bool HasTransactions(PXGraph graph, int? pmInstanceId)
  {
    return PXResultset<ExternalTransaction>.op_Implicit(PXSelectBase<ExternalTransaction, PXSelect<ExternalTransaction, Where<ExternalTransaction.pMInstanceID, Equal<Required<ExternalTransaction.pMInstanceID>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
    {
      (object) pmInstanceId
    })) != null;
  }

  public static bool IsExpired(IExternalTransaction extTran)
  {
    return extTran.ExpirationDate.HasValue && extTran.ExpirationDate.Value < PXTimeZoneInfo.Now && (extTran.ProcStatus == "AUS" || extTran.ProcStatus == "AUH" || extTran.ProcStatus == "CAH") || extTran.ProcStatus == "AUE" || extTran.ProcStatus == "CAE";
  }

  public static bool IsFailed(IExternalTransaction extTran)
  {
    return extTran.ProcStatus == "AUF" || extTran.ProcStatus == "AIF" || extTran.ProcStatus == "CAF" || extTran.ProcStatus == "VDF" || extTran.ProcStatus == "CDF";
  }

  public static ExternalTransactionState GetActiveTransactionState(
    PXGraph graph,
    PXSelectBase<ExternalTransaction> extTran)
  {
    IEnumerable<ExternalTransaction> extTrans = GraphHelper.RowCast<ExternalTransaction>((IEnumerable) extTran.Select(Array.Empty<object>()));
    return ExternalTranHelper.GetActiveTransactionState(graph, (IEnumerable<IExternalTransaction>) extTrans);
  }

  public static ExternalTransactionState GetLastTransactionState(
    PXGraph graph,
    PXSelectBase<ExternalTransaction> extTran)
  {
    IEnumerable<ExternalTransaction> extTrans = GraphHelper.RowCast<ExternalTransaction>((IEnumerable) extTran.Select(Array.Empty<object>()));
    return ExternalTranHelper.GetLastTransactionState(graph, (IEnumerable<IExternalTransaction>) extTrans);
  }

  public static ExternalTransactionState GetLastTransactionState(
    PXGraph graph,
    IEnumerable<IExternalTransaction> extTrans)
  {
    extTrans = (IEnumerable<IExternalTransaction>) extTrans.OrderByDescending<IExternalTransaction, int?>((Func<IExternalTransaction, int?>) (i => i.TransactionID));
    IExternalTransaction extTran = extTrans.FirstOrDefault<IExternalTransaction>();
    return extTran == null ? new ExternalTransactionState() : ExternalTranHelper.GetTransactionState(graph, extTran);
  }

  public static ExternalTransactionState GetTransactionState(
    PXGraph graph,
    IExternalTransaction extTran)
  {
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    if (extTran == null)
      throw new ArgumentNullException(nameof (extTran));
    ICCPaymentProcessingRepository repo = (ICCPaymentProcessingRepository) new CCPaymentProcessingRepository(graph);
    ExternalTransactionState transactionState = new ExternalTransactionState(extTran);
    ExternalTranHelper.CheckTranExpired(transactionState);
    if (transactionState.HasErrors)
    {
      if (transactionState.SyncFailed)
      {
        bool? active = extTran.Active;
        bool flag = false;
        if (active.GetValueOrDefault() == flag & active.HasValue)
          goto label_8;
      }
      ExternalTranHelper.ApplyLastSuccessfulTran(repo, transactionState);
    }
label_8:
    ExternalTranHelper.FormatDescription(graph, transactionState);
    return transactionState;
  }

  public static bool HasImportedNeedSyncTran(
    PXGraph graph,
    PXSelectBase<ExternalTransaction> extTrans)
  {
    IEnumerable<ExternalTransaction> extTrans1 = GraphHelper.RowCast<ExternalTransaction>((IEnumerable) extTrans.Select(Array.Empty<object>()));
    return ExternalTranHelper.HasImportedNeedSyncTran(graph, (IEnumerable<IExternalTransaction>) extTrans1);
  }

  public static bool HasImportedNeedSyncTran(
    PXGraph graph,
    IEnumerable<IExternalTransaction> extTrans)
  {
    bool flag = false;
    if (ExternalTranHelper.GetImportedNeedSyncTran(graph, extTrans) != null)
      flag = true;
    return flag;
  }

  public static IExternalTransaction GetImportedNeedSyncTran(
    PXGraph graph,
    IEnumerable<IExternalTransaction> extTrans)
  {
    return extTrans.OrderByDescending<IExternalTransaction, int?>((Func<IExternalTransaction, int?>) (i => i.TransactionID)).FirstOrDefault<IExternalTransaction>((Func<IExternalTransaction, bool>) (i =>
    {
      if (i.NeedSync.GetValueOrDefault())
        return true;
      return i.ProcStatus == "UKN" && i.Active.GetValueOrDefault();
    }));
  }

  public static ExternalTranHelper.SharedTranStatus GetSharedTranStatus(
    PXGraph graph,
    IExternalTransaction extTran)
  {
    ExternalTranHelper.SharedTranStatus sharedTranStatus = ExternalTranHelper.SharedTranStatus.None;
    if (extTran != null && extTran.VoidDocType == "REF")
    {
      if (extTran.SyncStatus == "E")
      {
        sharedTranStatus = ExternalTranHelper.SharedTranStatus.ClearState;
      }
      else
      {
        CCProcTran ccProcTran = new CCProcTranRepository(graph).GetCCProcTranByTranID(extTran.TransactionID).OrderByDescending<CCProcTran, int?>((Func<CCProcTran, int?>) (i => i.TranNbr)).Where<CCProcTran>((Func<CCProcTran, bool>) (i => i.ProcStatus == "FIN" && i.TranStatus == "APR")).FirstOrDefault<CCProcTran>();
        if (ccProcTran != null && ccProcTran.DocType != extTran.VoidDocType && ccProcTran.RefNbr != extTran.VoidRefNbr)
          sharedTranStatus = ExternalTranHelper.SharedTranStatus.ClearState;
      }
      if (sharedTranStatus != ExternalTranHelper.SharedTranStatus.ClearState)
      {
        ExternalTransactionState transactionState = ExternalTranHelper.GetTransactionState(graph, extTran);
        if (transactionState.NeedSync)
          sharedTranStatus = ExternalTranHelper.SharedTranStatus.NeedSynchronization;
        else if (transactionState.IsVoided && !transactionState.NeedSync)
          sharedTranStatus = ExternalTranHelper.SharedTranStatus.Synchronized;
      }
    }
    return sharedTranStatus;
  }

  public static bool HasOpenCCProcTran(PXGraph graph, IExternalTransaction extTran)
  {
    bool flag = false;
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    if (extTran == null)
      return false;
    if (extTran.ProcStatus == "UKN" && new CCProcTranRepository(graph).GetCCProcTranByTranID(extTran.TransactionID).FirstOrDefault<CCProcTran>()?.ProcStatus == "OPN")
      flag = true;
    return flag;
  }

  public static ExternalTransactionState GetActiveTransactionState(
    PXGraph graph,
    IEnumerable<IExternalTransaction> extTrans)
  {
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    if (extTrans == null)
      throw new ArgumentNullException(nameof (extTrans));
    ExternalTransactionState transactionState = new ExternalTransactionState();
    CCProcTranRepository procTranRepository = new CCProcTranRepository(graph);
    IExternalTransaction activeTransaction = ExternalTranHelper.GetActiveTransaction(extTrans);
    if (activeTransaction != null)
      transactionState = ExternalTranHelper.GetTransactionState(graph, activeTransaction);
    return transactionState;
  }

  public static ProcessingStatus GetPossibleErrorStatusForTran(ExternalTransactionState state)
  {
    if (state.HasErrors)
      return state.ProcessingStatus;
    if (state.IsCaptured)
      return ProcessingStatus.CaptureFail;
    if (state.IsPreAuthorized)
      return ProcessingStatus.AuthorizeFail;
    if (state.IsVoided)
      return ProcessingStatus.VoidFail;
    return state.IsRefunded ? ProcessingStatus.CreditFail : ProcessingStatus.Unknown;
  }

  public static IExternalTransaction GetLastProcessedExtTran(
    IEnumerable<IExternalTransaction> extTrans,
    IEnumerable<ICCPaymentTransaction> trans)
  {
    IExternalTransaction processedExtTran = (IExternalTransaction) null;
    ICCPaymentTransaction tran = trans.OrderByDescending<ICCPaymentTransaction, int?>((Func<ICCPaymentTransaction, int?>) (i => i.TranNbr)).FirstOrDefault<ICCPaymentTransaction>();
    if (tran != null)
      processedExtTran = extTrans.FirstOrDefault<IExternalTransaction>((Func<IExternalTransaction, bool>) (i =>
      {
        int? transactionId1 = i.TransactionID;
        int? transactionId2 = tran.TransactionID;
        return transactionId1.GetValueOrDefault() == transactionId2.GetValueOrDefault() & transactionId1.HasValue == transactionId2.HasValue;
      }));
    return processedExtTran;
  }

  private static void ApplyLastSuccessfulTran(
    ICCPaymentProcessingRepository repo,
    ExternalTransactionState state)
  {
    ICCPaymentTransaction paymentTransaction = ExternalTranHelper.LastSuccessfulCCProcTran(state.ExternalTransaction, repo);
    if (paymentTransaction == null)
      return;
    switch (paymentTransaction.TranType)
    {
      case "IAA":
      case "AUT":
        state.IsPreAuthorized = true;
        break;
      case "AAC":
      case "PAC":
      case "CAP":
        state.IsCaptured = true;
        break;
      case "CDT":
        state.IsRefunded = true;
        break;
    }
    state.IsOpenForReview = paymentTransaction.TranStatus == "HFR";
    ExternalTranHelper.CheckTranExpired(state);
  }

  public static ICCPaymentTransaction LastSuccessfulCCProcTran(
    IExternalTransaction tran,
    ICCPaymentProcessingRepository repo)
  {
    return CCProcTranHelper.FindCCLastSuccessfulTran(repo.GetCCProcTranByTranID(tran.TransactionID).Cast<ICCPaymentTransaction>());
  }

  private static void CheckTranExpired(ExternalTransactionState state)
  {
    if (!state.IsPreAuthorized && !state.IsCaptured || !ExternalTranHelper.IsExpired(state.ExternalTransaction))
      return;
    if (state.IsPreAuthorized)
      state.ProcessingStatus = ProcessingStatus.AuthorizeExpired;
    if (state.IsCaptured)
      state.ProcessingStatus = ProcessingStatus.CaptureExpired;
    state.IsPreAuthorized = false;
    state.IsCaptured = false;
    state.IsOpenForReview = false;
    state.HasErrors = false;
    state.IsExpired = true;
  }

  public static void FormatDescription(PXGraph graph, ExternalTransactionState extTranState)
  {
    string currStatus = (string) null;
    IExternalTransaction externalTransaction = extTranState.ExternalTransaction;
    if (externalTransaction == null)
      return;
    if (extTranState.SyncFailed)
    {
      currStatus = ExternalTranHelper.GetSyncFailedDescription(graph, extTranState);
    }
    else
    {
      ExtTransactionProcStatusCode.ListAttribute listAttribute = new ExtTransactionProcStatusCode.ListAttribute();
      string processingStatus = ExtTransactionProcStatusCode.GetProcStatusStrByProcessingStatus(extTranState.ProcessingStatus);
      if (!string.IsNullOrEmpty(processingStatus))
        currStatus = PXMessages.LocalizeNoPrefix(listAttribute.ValueLabelDic[processingStatus]);
    }
    if (!string.IsNullOrEmpty(currStatus))
    {
      ExternalTransactionState transactionState = ExternalTranHelper.GetParentExternalTransactionState(graph, externalTransaction);
      bool flag = ExternalTranHelper.CheckDocIsPayment(graph);
      currStatus = !extTranState.NeedSync ? (!flag || transactionState == null ? ExternalTranHelper.AppendPreviousDescription(graph, extTranState, currStatus) : ExternalTranHelper.AppendDescription(graph, transactionState, currStatus)) : ExternalTranHelper.AppendPreviousDescriptionForSyncTran(graph, extTranState, currStatus);
    }
    extTranState.Description = string.IsNullOrEmpty(currStatus) ? string.Empty : currStatus;
  }

  private static ExternalTransactionState GetParentExternalTransactionState(
    PXGraph graph,
    IExternalTransaction extTran)
  {
    ExternalTransaction extTran1 = ExternalTransaction.PK.Find(graph, extTran.ParentTranID);
    ExternalTransactionState transactionState = (ExternalTransactionState) null;
    if (extTran1 != null)
      transactionState = new ExternalTransactionState((IExternalTransaction) extTran1);
    return transactionState;
  }

  /// <summary>
  /// Retrieves a current record from the caches and checks its ARDocType value.
  /// This is necessary for setting the "Captured, Refunded" processing status of the document
  /// </summary>
  /// <param name="graph">Current graph</param>
  /// <returns></returns>
  private static bool CheckDocIsPayment(PXGraph graph)
  {
    return (graph.Caches[typeof (PX.Objects.AR.ARRegister)].Current is PX.Objects.AR.ARRegister current ? current.DocType : (string) null) == "PMT" || current?.DocType == "PPM";
  }

  private static string AppendPreviousDescriptionForSyncTran(
    PXGraph graph,
    ExternalTransactionState state,
    string currStatus)
  {
    string str1 = currStatus;
    if (state.ProcessingStatus == ProcessingStatus.Unknown && graph.Caches[typeof (ARPayment)].Current is ARPayment current && current.CCActualExternalTransactionID.HasValue)
    {
      bool flag = current.DocType != "REF";
      string str2 = (string) null;
      CCPaymentProcessingRepository repo = new CCPaymentProcessingRepository(graph);
      ICCPaymentTransaction successfulBeforeSyncTran = ExternalTranHelper.GetSuccessfulBeforeSyncTran(PXResultset<ExternalTransaction>.op_Implicit(PXSelectBase<ExternalTransaction, PXSelect<ExternalTransaction, Where<ExternalTransaction.transactionID, Equal<Required<ExternalTransaction.transactionID>>>>.Config>.Select(graph, new object[1]
      {
        (object) current.CCActualExternalTransactionID
      })), (ICCPaymentProcessingRepository) repo);
      if (flag && successfulBeforeSyncTran != null)
        str2 = ExternalTranHelper.GetPreviousStatusForSyncTran(successfulBeforeSyncTran.TranType);
      CCTranType? nullable = new CCTranType?();
      if (successfulBeforeSyncTran != null)
        nullable = new CCTranType?(CCTranTypeCode.GetTranTypeByTranTypeStr(successfulBeforeSyncTran.TranType));
      if (successfulBeforeSyncTran == null && current.DocType == "REF" || nullable.HasValue && (nullable.GetValueOrDefault() == CCTranType.AuthorizeOnly || CCTranTypeCode.IsCaptured(nullable.Value)))
        str1 = PXMessages.LocalizeNoPrefix(state.SyncFailed ? "Void/Refund Failed Validation" : "Void/Refund Pending Validation");
      if (successfulBeforeSyncTran == null && ExternalTranHelper.CheckDocIsPayment(graph))
        str1 = PXMessages.LocalizeNoPrefix(state.SyncFailed ? "Pre-Auth./Capture Failed Validation" : "Pre-Auth./Capture Pending Validation");
      if (str2 != null)
        str1 = $"{str2}; {str1}";
    }
    return str1;
  }

  private static ICCPaymentTransaction GetSuccessfulBeforeSyncTran(
    ExternalTransaction extTran,
    ICCPaymentProcessingRepository repo)
  {
    return !(extTran.ProcStatus == "UKN") ? ExternalTranHelper.LastSuccessfulCCProcTran((IExternalTransaction) extTran, repo) : CCProcTranHelper.FindCCLastSuccessfulTran((IEnumerable<ICCPaymentTransaction>) repo.GetCCProcTranByTranID(extTran.TransactionID).Cast<CCProcTran>().OrderByDescending<CCProcTran, int?>((Func<CCProcTran, int?>) (i => i.TranNbr)).SkipWhile<CCProcTran>((Func<CCProcTran, bool>) (i => i.TranType == "UKN")));
  }

  private static string AppendPreviousDescription(
    PXGraph graph,
    ExternalTransactionState extTranState,
    string currStatus)
  {
    if ((!extTranState.HasErrors ? 0 : (!extTranState.SyncFailed ? 1 : 0)) != 0)
      currStatus = ExternalTranHelper.AppendDescription(graph, extTranState, currStatus);
    return currStatus;
  }

  private static string AppendDescription(
    PXGraph graph,
    ExternalTransactionState extTranState,
    string currStatus)
  {
    CCPaymentProcessingRepository repo = new CCPaymentProcessingRepository(graph);
    ICCPaymentTransaction paymentTransaction = ExternalTranHelper.LastSuccessfulCCProcTran(extTranState.ExternalTransaction, (ICCPaymentProcessingRepository) repo);
    string str = (string) null;
    if (paymentTransaction != null)
      str = ExternalTranHelper.GetStatusByTranType(paymentTransaction.TranType);
    if (!string.IsNullOrEmpty(str))
      currStatus = $"{str}, {currStatus}";
    return currStatus;
  }

  public static bool UpdateCapturedState<T>(T doc, ExternalTransactionState tranState) where T : class, IBqlTable, ICCCapturePayment
  {
    bool flag1 = false;
    IExternalTransaction externalTransaction = tranState.ExternalTransaction;
    bool? isCcCaptured1 = doc.IsCCCaptured;
    bool isCaptured = tranState.IsCaptured;
    if (!(isCcCaptured1.GetValueOrDefault() == isCaptured & isCcCaptured1.HasValue))
    {
      doc.IsCCCaptured = new bool?(tranState.IsCaptured);
      flag1 = true;
    }
    if (tranState.IsCaptured)
    {
      doc.CuryCCCapturedAmt = externalTransaction.Amount;
      doc.IsCCCaptureFailed = new bool?(false);
      flag1 = true;
    }
    if (tranState.ProcessingStatus == ProcessingStatus.CaptureFail)
    {
      doc.IsCCCaptureFailed = new bool?(true);
      flag1 = true;
    }
    bool? isCcCaptured2 = doc.IsCCCaptured;
    bool flag2 = false;
    if (isCcCaptured2.GetValueOrDefault() == flag2 & isCcCaptured2.HasValue)
    {
      Decimal? curyCcCapturedAmt = doc.CuryCCCapturedAmt;
      Decimal num = 0M;
      if (!(curyCcCapturedAmt.GetValueOrDefault() == num & curyCcCapturedAmt.HasValue))
      {
        doc.CuryCCCapturedAmt = new Decimal?(0M);
        flag1 = true;
      }
    }
    return flag1;
  }

  public static bool UpdateCCPaymentState<T>(T doc, ExternalTransactionState tranState) where T : class, ICCAuthorizePayment, ICCCapturePayment
  {
    IExternalTransaction externalTransaction = tranState.ExternalTransaction;
    bool flag1 = false;
    bool? isCcAuthorized = doc.IsCCAuthorized;
    bool isPreAuthorized = tranState.IsPreAuthorized;
    bool? nullable1;
    if (isCcAuthorized.GetValueOrDefault() == isPreAuthorized & isCcAuthorized.HasValue)
    {
      nullable1 = doc.IsCCCaptured;
      bool isCaptured = tranState.IsCaptured;
      if (nullable1.GetValueOrDefault() == isCaptured & nullable1.HasValue)
        goto label_5;
    }
    if (tranState.ProcessingStatus != ProcessingStatus.VoidFail && tranState.ProcessingStatus != ProcessingStatus.CreditFail)
    {
      doc.IsCCAuthorized = new bool?(tranState.IsPreAuthorized);
      doc.IsCCCaptured = new bool?(tranState.IsCaptured);
      flag1 = true;
    }
    else
    {
      doc.IsCCAuthorized = new bool?(false);
      doc.IsCCCaptured = new bool?(false);
      flag1 = false;
    }
label_5:
    if (externalTransaction != null && tranState.IsPreAuthorized)
    {
      doc.CCAuthExpirationDate = externalTransaction.ExpirationDate;
      doc.CuryCCPreAuthAmount = externalTransaction.Amount;
      flag1 = true;
    }
    nullable1 = doc.IsCCAuthorized;
    bool flag2 = false;
    if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
    {
      DateTime? nullable2 = doc.CCAuthExpirationDate;
      if (!nullable2.HasValue)
      {
        Decimal? curyCcPreAuthAmount = doc.CuryCCPreAuthAmount;
        Decimal num = 0M;
        if (!(curyCcPreAuthAmount.GetValueOrDefault() > num & curyCcPreAuthAmount.HasValue))
          goto label_11;
      }
      // ISSUE: variable of a boxed type
      __Boxed<T> local = (object) doc;
      nullable2 = new DateTime?();
      DateTime? nullable3 = nullable2;
      local.CCAuthExpirationDate = nullable3;
      doc.CuryCCPreAuthAmount = new Decimal?(0M);
      flag1 = true;
    }
label_11:
    if (tranState.IsCaptured)
    {
      doc.CuryCCCapturedAmt = externalTransaction.Amount;
      doc.IsCCCaptureFailed = new bool?(false);
      flag1 = true;
    }
    if (tranState.ProcessingStatus == ProcessingStatus.CaptureFail)
    {
      doc.IsCCCaptureFailed = new bool?(true);
      flag1 = true;
    }
    nullable1 = doc.IsCCCaptured;
    bool flag3 = false;
    if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue)
    {
      Decimal? curyCcCapturedAmt = doc.CuryCCCapturedAmt;
      Decimal num = 0M;
      if (!(curyCcCapturedAmt.GetValueOrDefault() == num & curyCcCapturedAmt.HasValue))
      {
        doc.CuryCCCapturedAmt = new Decimal?(0M);
        flag1 = true;
      }
    }
    return flag1;
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R1.")]
  public static IEnumerable<ExternalTransaction> GetSOInvoiceExternalTrans(
    PXGraph graph,
    PX.Objects.AR.ARInvoice currentInvoice)
  {
    PXGraph pxGraph1 = graph;
    object[] objArray1 = new object[1]
    {
      (object) currentInvoice
    };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<ExternalTransaction> pxResult in PXSelectBase<ExternalTransaction, PXSelectReadonly<ExternalTransaction, Where<ExternalTransaction.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<ExternalTransaction.docType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>>>, OrderBy<Desc<ExternalTransaction.transactionID>>>.Config>.SelectMultiBound(pxGraph1, objArray1, objArray2))
      yield return PXResult<ExternalTransaction>.op_Implicit(pxResult);
    PXGraph pxGraph2 = graph;
    object[] objArray3 = new object[1]
    {
      (object) currentInvoice
    };
    object[] objArray4 = Array.Empty<object>();
    foreach (PXResult<ExternalTransaction> pxResult in PXSelectBase<ExternalTransaction, PXSelectReadonly2<ExternalTransaction, InnerJoin<PX.Objects.SO.SOOrderShipment, On<PX.Objects.SO.SOOrderShipment.orderNbr, Equal<ExternalTransaction.origRefNbr>, And<PX.Objects.SO.SOOrderShipment.orderType, Equal<ExternalTransaction.origDocType>>>>, Where<PX.Objects.SO.SOOrderShipment.invoiceNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<PX.Objects.SO.SOOrderShipment.invoiceType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<ExternalTransaction.refNbr, IsNull>>>, OrderBy<Desc<CCProcTran.tranNbr>>>.Config>.SelectMultiBound(pxGraph2, objArray3, objArray4))
      yield return PXResult<ExternalTransaction>.op_Implicit(pxResult);
  }

  private static string GetSyncFailedDescription(PXGraph graph, ExternalTransactionState state)
  {
    string currStatus = (string) null;
    switch (state.ProcessingStatus)
    {
      case ProcessingStatus.Unknown:
        currStatus = PXMessages.LocalizeNoPrefix("Validation Failed");
        break;
      case ProcessingStatus.AuthorizeFail:
        currStatus = PXMessages.LocalizeNoPrefix("Validation Failed (Authorization)");
        break;
      case ProcessingStatus.CaptureFail:
        currStatus = PXMessages.LocalizeNoPrefix("Validation Failed (Capture)");
        break;
      case ProcessingStatus.VoidFail:
        currStatus = PXMessages.LocalizeNoPrefix("Validation Failed (Voiding)");
        break;
      case ProcessingStatus.CreditFail:
        currStatus = PXMessages.LocalizeNoPrefix("Validation Failed (Refund)");
        break;
    }
    return ExternalTranHelper.AppendPreviousDescriptionForSyncTran(graph, state, currStatus);
  }

  private static string GetPreviousStatusForSyncTran(string tranType)
  {
    string statusForSyncTran = (string) null;
    switch (tranType)
    {
      case "IAA":
      case "AUT":
        statusForSyncTran = PXMessages.LocalizeNoPrefix("Pre-Auth.");
        break;
      case "PAC":
      case "AAC":
      case "CAP":
        statusForSyncTran = PXMessages.LocalizeNoPrefix("Captured");
        break;
    }
    return statusForSyncTran;
  }

  private static string GetStatusByTranType(string tranType)
  {
    string statusByTranType = (string) null;
    if (tranType != null && tranType.Length == 3)
    {
      switch (tranType[0])
      {
        case 'A':
          switch (tranType)
          {
            case "AUT":
              break;
            case "AAC":
              goto label_9;
            default:
              goto label_13;
          }
          break;
        case 'C':
          switch (tranType)
          {
            case "CAP":
              goto label_9;
            case "CDT":
              statusByTranType = PXMessages.LocalizeNoPrefix("Refunded");
              goto label_13;
            default:
              goto label_13;
          }
        case 'I':
          if (tranType == "IAA")
            break;
          goto label_13;
        case 'P':
          if (tranType == "PAC")
            goto label_9;
          goto label_13;
        case 'R':
          if (tranType == "REJ")
          {
            statusByTranType = PXMessages.LocalizeNoPrefix("Rejected");
            goto label_13;
          }
          goto label_13;
        case 'V':
          if (tranType == "VDG")
          {
            statusByTranType = PXMessages.LocalizeNoPrefix("Voided");
            goto label_13;
          }
          goto label_13;
        default:
          goto label_13;
      }
      statusByTranType = PXMessages.LocalizeNoPrefix("Pre-Authorized");
      goto label_13;
label_9:
      statusByTranType = PXMessages.LocalizeNoPrefix("Captured");
    }
label_13:
    return statusByTranType;
  }

  public enum SharedTranStatus
  {
    None,
    NeedSynchronization,
    Synchronized,
    ClearState,
  }
}
