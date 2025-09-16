// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Repositories.ExternalTransactionRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.Repositories;

public class ExternalTransactionRepository
{
  protected readonly PXGraph graph;

  public ExternalTransactionRepository(PXGraph graph)
  {
    this.graph = graph ?? throw new ArgumentNullException(nameof (graph));
  }

  public ExternalTransaction FindCapturedExternalTransaction(int? pMInstanceID, string tranNbr)
  {
    if (!pMInstanceID.HasValue)
      throw new ArgumentNullException(nameof (pMInstanceID));
    if (string.IsNullOrEmpty(tranNbr))
      throw new ArgumentException(nameof (tranNbr));
    return ((PXSelectBase<ExternalTransaction>) new PXSelect<ExternalTransaction, Where<ExternalTransaction.pMInstanceID, Equal<Required<ExternalTransaction.pMInstanceID>>, And<ExternalTransaction.tranNumber, Equal<Required<ExternalTransaction.tranNumber>>, And<Where<ExternalTransaction.procStatus, Equal<ExtTransactionProcStatusCode.captureSuccess>, Or<ExternalTransaction.procStatus, Equal<ExtTransactionProcStatusCode.captureHeldForReview>>>>>>, OrderBy<Desc<ExternalTransaction.transactionID>>>(this.graph)).SelectSingle(new object[2]
    {
      (object) pMInstanceID,
      (object) tranNbr
    });
  }

  public Tuple<ExternalTransaction, ARPayment> GetExternalTransactionWithPayment(
    string tranNbr,
    string procCenterId)
  {
    Tuple<ExternalTransaction, ARPayment> transactionWithPayment = (Tuple<ExternalTransaction, ARPayment>) null;
    PXResult<ExternalTransaction, ARPayment> pxResult = (PXResult<ExternalTransaction, ARPayment>) PXResultset<ExternalTransaction>.op_Implicit(((PXSelectBase<ExternalTransaction>) new PXSelectJoin<ExternalTransaction, InnerJoin<ARPayment, On<ExternalTransaction.docType, Equal<ARPayment.docType>, And<ExternalTransaction.refNbr, Equal<ARPayment.refNbr>>>>, Where<ExternalTransaction.tranNumber, Equal<Required<ExternalTransaction.tranNumber>>, And<ExternalTransaction.processingCenterID, Equal<Required<ExternalTransaction.processingCenterID>>, And<Not<ExternalTransaction.syncStatus, Equal<CCSyncStatusCode.error>, And<ExternalTransaction.active, Equal<False>>>>>>, OrderBy<Desc<ExternalTransaction.transactionID>>>(this.graph)).Select(new object[2]
    {
      (object) tranNbr,
      (object) procCenterId
    }));
    if (pxResult != null)
      transactionWithPayment = new Tuple<ExternalTransaction, ARPayment>(PXResult<ExternalTransaction, ARPayment>.op_Implicit(pxResult), PXResult<ExternalTransaction, ARPayment>.op_Implicit(pxResult));
    return transactionWithPayment;
  }

  public Tuple<ExternalTransaction, ARPayment> GetExternalTransactionWithPaymentByApiNumber(
    string tranApiNbr,
    string procCenterId)
  {
    Tuple<ExternalTransaction, ARPayment> paymentByApiNumber = (Tuple<ExternalTransaction, ARPayment>) null;
    PXResult<ExternalTransaction, ARPayment> pxResult = (PXResult<ExternalTransaction, ARPayment>) PXResultset<ExternalTransaction>.op_Implicit(((PXSelectBase<ExternalTransaction>) new PXSelectJoin<ExternalTransaction, InnerJoin<ARPayment, On<ExternalTransaction.docType, Equal<ARPayment.docType>, And<ExternalTransaction.refNbr, Equal<ARPayment.refNbr>>>>, Where<ExternalTransaction.tranApiNumber, Equal<Required<ExternalTransaction.tranApiNumber>>, And<ExternalTransaction.processingCenterID, Equal<Required<ExternalTransaction.processingCenterID>>, And<Not<ExternalTransaction.syncStatus, Equal<CCSyncStatusCode.error>, And<ExternalTransaction.active, Equal<False>>>>>>, OrderBy<Desc<ExternalTransaction.transactionID>>>(this.graph)).Select(new object[2]
    {
      (object) tranApiNbr,
      (object) procCenterId
    }));
    if (pxResult != null)
      paymentByApiNumber = new Tuple<ExternalTransaction, ARPayment>(PXResult<ExternalTransaction, ARPayment>.op_Implicit(pxResult), PXResult<ExternalTransaction, ARPayment>.op_Implicit(pxResult));
    return paymentByApiNumber;
  }

  public ExternalTransaction FindCapturedExternalTransaction(string procCenterId, string tranNbr)
  {
    if (string.IsNullOrEmpty(procCenterId))
      throw new ArgumentException(nameof (procCenterId));
    if (string.IsNullOrEmpty(tranNbr))
      throw new ArgumentException(nameof (tranNbr));
    return ((PXSelectBase<ExternalTransaction>) new PXSelect<ExternalTransaction, Where<ExternalTransaction.tranNumber, Equal<Required<ExternalTransaction.tranNumber>>, And<ExternalTransaction.processingCenterID, Equal<Required<ExternalTransaction.processingCenterID>>, And<Where<ExternalTransaction.procStatus, Equal<ExtTransactionProcStatusCode.captureSuccess>, Or<ExternalTransaction.procStatus, Equal<ExtTransactionProcStatusCode.captureHeldForReview>>>>>>, OrderBy<Desc<ExternalTransaction.transactionID>>>(this.graph)).SelectSingle(new object[2]
    {
      (object) tranNbr,
      (object) procCenterId
    });
  }

  public IEnumerable<ExternalTransaction> GetExternalTransactionsByPayLinkID(int? payLinkId)
  {
    return GraphHelper.RowCast<ExternalTransaction>((IEnumerable) PXSelectBase<ExternalTransaction, PXSelect<ExternalTransaction, Where<ExternalTransaction.payLinkID, Equal<Required<ExternalTransaction.payLinkID>>>>.Config>.Select(this.graph, new object[1]
    {
      (object) payLinkId
    }));
  }

  public IEnumerable<ExternalTransaction> GetExternalTransaction(
    string cCProcessingCenterID,
    string tranNumber)
  {
    return GraphHelper.RowCast<ExternalTransaction>((IEnumerable) PXSelectBase<ExternalTransaction, PXSelectJoin<ExternalTransaction, LeftJoin<CustomerPaymentMethod, On<CustomerPaymentMethod.pMInstanceID, Equal<ExternalTransaction.pMInstanceID>>>, Where<ExternalTransaction.tranNumber, Equal<Required<ExternalTransaction.tranNumber>>, And<Where<ExternalTransaction.processingCenterID, Equal<Required<ExternalTransaction.processingCenterID>>, Or<CustomerPaymentMethod.cCProcessingCenterID, Equal<Required<CustomerPaymentMethod.cCProcessingCenterID>>>>>>>.Config>.Select(this.graph, new object[3]
    {
      (object) tranNumber,
      (object) cCProcessingCenterID,
      (object) cCProcessingCenterID
    }));
  }

  public ExternalTransaction GetExternalTransactionByNoteID(Guid? noteID)
  {
    return PXResultset<ExternalTransaction>.op_Implicit(PXSelectBase<ExternalTransaction, PXSelect<ExternalTransaction, Where<ExternalTransaction.noteID, Equal<Required<ExternalTransaction.noteID>>>>.Config>.Select(this.graph, new object[1]
    {
      (object) noteID
    }));
  }

  public ExternalTransaction InsertExternalTransaction(ExternalTransaction extTran)
  {
    return this.graph.Caches[typeof (ExternalTransaction)].Insert((object) extTran) as ExternalTransaction;
  }

  public ExternalTransaction UpdateExternalTransaction(ExternalTransaction extTran)
  {
    return this.graph.Caches[typeof (ExternalTransaction)].Update((object) extTran) as ExternalTransaction;
  }
}
